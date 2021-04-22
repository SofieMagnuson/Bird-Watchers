using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody RB, skullRB;
    public int health, pecks, peckAmountToKill;
    public float speed, ascendSpeed, turnSpeed, attackSpeed, waitUntilAttack, descendSpeed, lookAtTargetSpeed, TStimer, maxVelocity, waitUntilMoving, maxHeight, tilt, maxTilt, tiltSpeed;
    public bool isGrounded, isAscending, targetIsSet, reachedTarget, reachedSkull, collided;
    public LayerMask clickLayer;
    public Vector3 target, respawnPos;
    public Transform targ, human1, human2, human3, target1, target2, target3, skull, skullObj;
    public Camera cam;
    public CameraMovement camScript;
    [Range(-10.0f, 0.0f)]
    public float maxFallSpeed;
    [Range(0.0f, 10.0f)]
    public float maxAscendSpeed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        health = 3;
        respawnPos = new Vector3(-1.7f, 14.6f, -655.4f);
        ascendSpeed = 0.8f;
        descendSpeed = -2f;
        //turnspeed = 1.3f;
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
        tilt = 0;
        maxTilt = 20;
        tiltSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
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
                        else if (hit.collider.gameObject.name == "skull")
                        {
                            targ = skull;
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
                    human1.gameObject.SetActive(false);
                }
                else if (targ == target2)
                {
                    human2.gameObject.SetActive(false);
                }
                else if (targ == target3)
                {
                    human3.gameObject.SetActive(false);
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

        }
    }

    private void FixedUpdate()
    {

        if (!targetIsSet)
        {
            if (transform.rotation.eulerAngles.x != 0 && transform.rotation.eulerAngles.z != 0)
            {
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }


            #region movement

            //Vector3 newVelocity = RB.velocity + (transform.forward * speed) * (1f - Vector3.Dot(RB.velocity, transform.forward) / speed);
            //newVelocity.y = Mathf.Clamp(newVelocity.y, maxFallSpeed, maxAscendSpeed);
            //if (newVelocity.magnitude > maxVelocity)
            Vector3 locVel = transform.InverseTransformDirection(RB.velocity);
            locVel.z = speed;
            locVel.x = 0;
            locVel.y = Mathf.Clamp(locVel.y, maxFallSpeed, maxAscendSpeed);
            RB.velocity = transform.TransformDirection(locVel);

            if (Input.GetKey(KeyCode.W))
            {
                isAscending = true;
                RB.AddForce(new Vector3(0, ascendSpeed, 0), ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.S))
            {
                RB.AddForce(new Vector3(0, descendSpeed, 0), ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.A))
            {
                float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
                RB.AddTorque(transform.up * turn, ForceMode.VelocityChange);
                //tilt = Mathf.Min(tilt + tiltSpeed * Time.deltaTime, maxTilt);
                //transform.Rotate(transform.rotation.x, transform.rotation.y, tilt);
                if (RB.angularVelocity.y <= -maxVelocity)
                {
                    RB.angularVelocity = new Vector3(RB.angularVelocity.x, -maxVelocity, RB.angularVelocity.z);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
                RB.AddTorque(transform.up * turn, ForceMode.VelocityChange);
                //tilt = Mathf.Max(tilt - tiltSpeed * Time.deltaTime, -maxTilt);
                //transform.Rotate(transform.rotation.x, transform.rotation.y, tilt);
                if (RB.angularVelocity.y >= maxVelocity)
                {
                    RB.angularVelocity = new Vector3(RB.angularVelocity.x, maxVelocity, RB.angularVelocity.z);
                }
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                RB.angularVelocity = new Vector3(0, 0, 0);
                //if (tilt != 0)
                //{
                //    tilt = tilt < 0 ? Mathf.Min(tilt + tiltSpeed * 2 * Time.deltaTime, 0) : Mathf.Max(tilt - tiltSpeed * 2 * Time.deltaTime, 0);
                //}
            }
            
            isAscending = false;
            #endregion

            #region pickUp

            if (reachedSkull && Input.GetMouseButton(0))
            {
                skullRB.useGravity = false;
                skullObj.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            }
            if (reachedSkull && Input.GetMouseButtonUp(0))
            {
                skullRB.useGravity = true;
            }
            if (reachedSkull && Input.GetKey(KeyCode.W))
            {
                RB.constraints = RigidbodyConstraints.None;
                isAscending = true;
                RB.AddForce(new Vector3(0, ascendSpeed * 2f, 0), ForceMode.Impulse);
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

    private void PickUpObject()
    {

    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Human" || col.gameObject.name == "Human2" || col.gameObject.name == "Human3")
        {
            reachedTarget = true;
        }
        if (col.gameObject.name == "skull")
        {
            reachedSkull = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Human" || col.gameObject.name == "Human2" || col.gameObject.name == "Human3")
        {
            reachedTarget = false;
        }
        if (col.gameObject.name == "skull")
        {
            reachedSkull = false;
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