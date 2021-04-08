using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody RB;
    public float speed, ascendSpeed, maxVelocity, turnSpeed, attackSpeed, waitUntilAttack, descendSpeed;
    public bool isGrounded, isAscending, targetIsSet;
    public LayerMask clickLayer;
    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        ascendSpeed = 0.8f;
        descendSpeed = -1f;
        turnSpeed = 60f;
        attackSpeed = 0.7f;
        waitUntilAttack = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            RB.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
        {
            #region set target
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = -Vector3.one;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100f, clickLayer))
                {
                    mousePos = hit.point;
                    target = mousePos;
                    target.y = mousePos.y + 1;
                    targetIsSet = true;
                    //kolla i framtiden if target.tag eller name är blabla osv.
                }

            }

            #endregion
            if (!targetIsSet)
            {
                #region movement
                if (isAscending)
                {
                    maxVelocity = 5f;
                }
                if (!isAscending)
                {
                    maxVelocity = 3f;
                }

                Vector3 newVelocity = RB.velocity + (transform.forward * speed) * (1f - Vector3.Dot(RB.velocity, transform.forward) / speed);
                if (Input.GetKey(KeyCode.S))
                {
                    RB.AddForce(new Vector3(0, descendSpeed, 0), ForceMode.Impulse);
                }
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
                }
                isAscending = false;

                if (newVelocity.magnitude > maxVelocity)
                {
                    newVelocity = newVelocity.normalized * maxVelocity;
                }
                RB.velocity = newVelocity;

                if (Input.GetKey(KeyCode.Space))
                {
                    isAscending = true;
                    RB.AddForce(new Vector3(0, ascendSpeed, 0), ForceMode.Impulse);
                }

                //RB.velocity = (transform.forward * speed) + (-transform.up * ascendSpeed);

                #endregion
            }
            else
            {
                RB.isKinematic = true;
                waitUntilAttack -= Time.deltaTime;
                if (waitUntilAttack <= 0)
                {
                    Attack();
                }
            }

        }
    }

    public void Attack()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(target.x, target.y, target.z);
        transform.position = Vector3.MoveTowards(startPos, endPos, attackSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "ground")
        {
            isGrounded = false;
        }
    }
}
