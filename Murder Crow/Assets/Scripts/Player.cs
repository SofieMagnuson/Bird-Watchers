using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody RB, skullRB;
    public int health, pecks, peckAmountToKill;
    public float speed, ascendSpeed, turnSpeed, attackSpeed, waitUntilAttack, descendSpeed, lookAtTargetSpeed, TStimer, maxVelocity, waitUntilMoving, maxHeight, maxTilt, tiltSpeed;
    public float tiltZ, tiltX;
    public bool isGrounded, isAscending, targetIsSet, reachedTarget, reachedSkull, collided, inDropZone;
    public bool inWindZone = false;
    public LayerMask clickLayer;
    public Vector3 target, respawnPos, angles, skullPickup;
    public Transform targ, human1, human2, human3, target1, target2, target3;
    public Camera cam;
    public CameraMovement camScript;
    [Range(-10.0f, 0.0f)]
    public float maxFallSpeed;
    [Range(0.0f, 10.0f)]
    public float maxAscendSpeed, rotZ;
    public Animator anim;
    public GameObject skull;
    public GameObject WindZone;


    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        health = 3;
        respawnPos = new Vector3(-1.7f, 14.6f, -655.4f);
        ascendSpeed = 0.8f;
        descendSpeed = -2f;
        turnSpeed = 2.0f;
        attackSpeed = 0.5f;
        waitUntilAttack = 2f;
        waitUntilMoving = 2f;
        lookAtTargetSpeed = 2f;
        TStimer = 3f;
        maxVelocity = 2f;
        maxHeight = 25f;
        pecks = 0;
        peckAmountToKill = 10;
        tiltZ = 0;
        tiltX = 0;
        maxTilt = 20;
        tiltSpeed = 30;
        skullPickup = new Vector3(0, -0.206f, 0);
        RB = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        
        if (isGrounded)
        {
            RB.constraints = RigidbodyConstraints.FreezeAll;
            if (Input.GetKey(KeyCode.W))
            {
                RB.constraints = RigidbodyConstraints.None;
                isAscending = true;
                RB.AddForce(new Vector3(0, ascendSpeed, 0), ForceMode.Impulse);
            }
        }
        else
        {
            if (transform.position.y >= maxHeight && Input.GetKey(KeyCode.W))
            {
                maxAscendSpeed = 0;
            }
            else
            {
                maxAscendSpeed = 3.5f;
            }

            #region set target
            if (!targetIsSet && !reachedTarget)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mousePos = -Vector3.one;

                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100f, clickLayer))
                    {
                        mousePos = hit.point;
                        if (hit.collider.gameObject.name == "Human")
                        {
                            targ = target1;
                            camScript.attackTarget = camScript.attackTarget1;
                        }
                        else if (hit.collider.gameObject.name == "Human2")
                        {
                            targ = target2;
                            camScript.attackTarget = camScript.attackTarget2;
                        }
                        else if (hit.collider.gameObject.name == "Human3")
                        {
                            targ = target3;
                            camScript.attackTarget = camScript.attackTarget3;
                        }
                        targetIsSet = true;
                    }
                }
            }
            #endregion

            #region attacking

            if (pecks == peckAmountToKill)
            {
                if (targ == target1)
                {
                    skull = SpawnSkull("Prefabs/skull", new Vector3(human1.position.x, human1.position.y + 1f, human1.position.z));
                    human1.gameObject.SetActive(false);
                    targ = null;
                }
                else if (targ == target2)
                {
                    skull = SpawnSkull("Prefabs/skull", new Vector3(human2.position.x, human2.position.y + 1f, human2.position.z));
                    human2.gameObject.SetActive(false);
                    targ = null;
                }
                else if (targ == target3)
                {
                    skull = SpawnSkull("Prefabs/skull", new Vector3(human3.position.x, human3.position.y + 1f, human3.position.z));
                    human3.gameObject.SetActive(false);
                    targ = null;
                }
                reachedTarget = false;
                waitUntilMoving -= Time.deltaTime;
                if (waitUntilMoving <= 0)
                {
                    RB.constraints = RigidbodyConstraints.None;
                    waitUntilMoving = 2f;
                    pecks = 0;
                }
            }

            if (reachedTarget && Input.GetMouseButtonDown(0))
            {
                if (pecks < peckAmountToKill)
                {
                    pecks += 1;
                }
            }
            #endregion

            if (reachedTarget && Input.GetKey(KeyCode.W))
            {
                RB.constraints = RigidbodyConstraints.None;
                isAscending = true;
                RB.AddForce(new Vector3(0, ascendSpeed * 2f, 0), ForceMode.Impulse);
            }

            if (collided)
            {
                waitUntilMoving -= Time.deltaTime;
                if (waitUntilMoving > 0)
                {
                    RB.constraints = RigidbodyConstraints.FreezeAll;
                    camScript.offset = camScript.noMovingOffset;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 51.084f, 0);
                    RB.constraints = RigidbodyConstraints.None;
                    camScript.offset = camScript.flyingOffset;
                    waitUntilMoving = 2f;
                    collided = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                anim.Play("Take 001");
            }

        }
    }

    private void FixedUpdate()
    {

        if (!targetIsSet)
        {

            #region movement

            Vector3 locVel = transform.InverseTransformDirection(RB.velocity);
            locVel.z = speed;
            locVel.x = 0;
            locVel.y = Mathf.Clamp(locVel.y, maxFallSpeed, maxAscendSpeed);
            RB.velocity = transform.TransformDirection(locVel);

           
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                RB.angularVelocity = new Vector3(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                isAscending = true;
                RB.AddForce(new Vector3(0, ascendSpeed, 0), ForceMode.Impulse);
                if (transform.position.y < maxHeight - 2)
                {
                    tiltX = Mathf.Max(tiltX - 20 * Time.deltaTime, -maxTilt);
                }
                else
                {
                    tiltX = Mathf.Min(tiltX + tiltSpeed * 2 * Time.deltaTime, 0);
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                tiltX = Mathf.Min(tiltX + 20 * Time.deltaTime, maxTilt);
                RB.AddForce(new Vector3(0, descendSpeed, 0), ForceMode.Impulse);
            }
            else if (tiltX != 0)
            {
                tiltX = tiltX < 0 ? Mathf.Min(tiltX + tiltSpeed * 2 * Time.deltaTime, 0) : Mathf.Max(tiltX - tiltSpeed * 2 * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
                tiltZ = Mathf.Min(tiltZ + tiltSpeed * Time.deltaTime, maxTilt);
                RB.AddTorque(transform.up * turn, ForceMode.VelocityChange);
                if (RB.angularVelocity.y <= -maxVelocity)
                {
                    RB.angularVelocity = new Vector3(RB.angularVelocity.x, -maxVelocity, RB.angularVelocity.z);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
                tiltZ = Mathf.Max(tiltZ - tiltSpeed * Time.deltaTime, -maxTilt);
                RB.AddTorque(transform.up * turn, ForceMode.VelocityChange);
                if (RB.angularVelocity.y >= maxVelocity)
                {
                    RB.angularVelocity = new Vector3(RB.angularVelocity.x, maxVelocity, RB.angularVelocity.z);
                }
            }
            else if (tiltZ != 0)
            {
                tiltZ = tiltZ < 0 ? Mathf.Min(tiltZ + tiltSpeed * 2 * Time.deltaTime, 0) : Mathf.Max(tiltZ - tiltSpeed * 2 * Time.deltaTime, 0);
            }

            angles = new Vector3(tiltX, transform.eulerAngles.y, tiltZ);
            transform.rotation = Quaternion.Euler(angles);

            
            isAscending = false;
            #endregion

            #region pickUp

            if (reachedSkull && inDropZone)
            {
                reachedSkull = false;
                skull.transform.parent = null;
                skullRB.useGravity = true;
                Debug.Log("inne");
            }
            if (reachedSkull)
            {
                PickUpSkull();
            }
            if (inWindZone)
            {
                RB.AddForce(WindZone.GetComponent<WindArea>().direction * WindZone.GetComponent <WindArea>().strength);
            }

            #endregion

        }
        else
        {
            RB.constraints = RigidbodyConstraints.FreezePosition;
            target = targ.position;
            Vector3 dir = target - transform.position;
            dir.y = 0f;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, lookAtTargetSpeed * Time.deltaTime);
            waitUntilAttack -= Time.deltaTime;
            if (waitUntilAttack <= 0)
            {
                Attack();
            }
        }
    }

    public void Attack()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(target.x, target.y, target.z);
        transform.position = Vector3.MoveTowards(startPos, endPos, attackSpeed);
        if (startPos == endPos)
        {
            targetIsSet = false;
            waitUntilAttack = 2f;
        }
    }

    public GameObject SpawnSkull(string prefab, Vector3 pos)
    {
        GameObject skull = Resources.Load<GameObject>(prefab);
        GameObject instance = GameObject.Instantiate(skull);
        instance.transform.position = pos;

        return instance;
    }

    private void PickUpSkull()
    {
        skull.transform.parent = transform;
        skull.transform.localPosition = skullPickup;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Human" || col.gameObject.name == "Human2" || col.gameObject.name == "Human3")
        {
            reachedTarget = true;
        }
        if (col.gameObject.name == "skull(Clone)")
        {
            reachedSkull = true;
        }
        if (col.gameObject.name == "DropSkullArea")
        {
            inDropZone = true;
        }
        if (col.gameObject.tag == "windArea")
        {
            WindZone = col.gameObject;
            inWindZone = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Human" || col.gameObject.name == "Human2" || col.gameObject.name == "Human3")
        {
            reachedTarget = false;
        }
        if (col.gameObject.name == "skull(Clone)")
        {
            reachedSkull = false;
        }
        if (col.gameObject.name == "DropSkullArea")
        {
            inDropZone = false;
        }
        if (col.gameObject.tag == "windArea")
        {
            WindZone = col.gameObject;
            inWindZone = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "terrain")
        {
            isGrounded = true;
        }
        if (col.gameObject.tag == "obstacles")
        {
            transform.position = respawnPos;
            health -= 1;
            collided = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "terrain")
        {
            isGrounded = false;
        }
    }
}