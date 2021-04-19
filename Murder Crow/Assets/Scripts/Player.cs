using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody RB;
    public int health;
    public float speed, ascendSpeed, turnSpeed, attackSpeed, waitUntilAttack, descendSpeed, lookAtTargetSpeed, TStimer, maxVelocity, waitUntilMoving;
    public bool isGrounded, isAscending, targetIsSet, reachedTarget, collided;
    public LayerMask clickLayer;
    public Vector3 target, respawnPos;
    public Transform targ, human1, human2, human3;
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
        turnSpeed = 1.3f; 
        attackSpeed = 0.5f;
        waitUntilAttack = 2f;
        waitUntilMoving = 2f;
        lookAtTargetSpeed = 2f;
        TStimer = 3f;
        maxVelocity = 2f;
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
                //targetIsSet = false;  // ändra denna senare
            }
        }
        else
        {
            #region set target
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
                        targ = human1;
                    }
                    if (hit.collider.gameObject.name == "Human2")
                    {
                        targ = human2;
                    }
                    if (hit.collider.gameObject.name == "Human3")
                    {
                        targ = human3;
                    }
                    targetIsSet = true;
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
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                RB.angularVelocity = new Vector3(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
                RB.AddTorque(transform.up * turn, ForceMode.VelocityChange);
                if (RB.angularVelocity.y <= -maxVelocity)
                {
                    RB.angularVelocity = new Vector3(RB.angularVelocity.x, -maxVelocity, RB.angularVelocity.z);
                }

            }
            if (Input.GetKey(KeyCode.D))
            {
                float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
                RB.AddTorque(transform.up * turn, ForceMode.VelocityChange);
                if (RB.angularVelocity.y >= maxVelocity)
                {
                    RB.angularVelocity = new Vector3(RB.angularVelocity.x, maxVelocity, RB.angularVelocity.z);
                }
            }
            isAscending = false;

            #endregion
        }
        else
        {
            RB.constraints = RigidbodyConstraints.FreezePosition;
            target = targ.position;
            target.y = targ.position.y + 1.8f;
            if (targ == human1)
            {
                target.x = targ.localPosition.x + 0.2f;
                target.z = targ.localPosition.z + 0.2f;
            }
            else if (targ == human2)
            {
                target.x = targ.localPosition.x - 0.2f;
                target.z = targ.localPosition.z - 0.2f;
            }
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


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Human" || col.gameObject.name == "Human2" || col.gameObject.name == "Human3")
        {
            reachedTarget = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Human" || col.gameObject.name == "Human2" || col.gameObject.name == "Human3")
        {
            reachedTarget = false;
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
