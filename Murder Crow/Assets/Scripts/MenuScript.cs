using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Rigidbody RB;
    public float ascendSpeed, attackSpeed, waitUntilAttack, descendSpeed, lookAtTargetSpeed, speed, TStimer, maxVelocity;
    public bool isGrounded, isAscending, targetIsSet, reachedTarget;
    public LayerMask clickLayer;
    public Vector3 target;
    public Transform targ, start, option, quit;
    public float maxFallSpeed;
    [Range(0.0f, 10.0f)]
    public float maxAscendSpeed;
    public Camera cam;

    public void Start()
    {
        attackSpeed = 0.5f;
        waitUntilAttack = 2f;
        lookAtTargetSpeed = 2f;
        TStimer = 3f;
    }

    void Update()
    {
        if (isGrounded)
        {
            RB.constraints = RigidbodyConstraints.FreezePosition;
            if (Input.GetKey(KeyCode.W))
            {
                RB.constraints = RigidbodyConstraints.None;
                isAscending = true;
                RB.AddForce(new Vector3(0, ascendSpeed * 2f, 0), ForceMode.Impulse);
                //targetIsSet = false;  // ändra denna senare
            }
        }
        else
       
        {
            #region set target
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = -Vector3.one;

                RaycastHit hit;

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100f, clickLayer))
                {
                    mousePos = hit.point;
                    if (hit.collider.gameObject.name == "Start")
                    {
                        targ = start;
                    }
                    if (hit.collider.gameObject.name == "Option")
                    {
                       targ = option;
                    }
                    if (hit.collider.gameObject.name == "Quit")
                    {
                        targ = quit;
                    }
                    
                    targetIsSet = true;
                }

            }

            #endregion

        }
    }

    private void FixedUpdate()
    {

        if (!targetIsSet)
        {

            #region movement

            //Vector3 newVelocity = RB.velocity + (transform.forward * speed) * (1f - Vector3.Dot(RB.velocity, transform.forward) / speed);
            //newVelocity.y = Mathf.Clamp(newVelocity.y, maxFallSpeed, maxAscendSpeed);
            //if (newVelocity.magnitude > maxVelocity)
            Vector3 locVel = transform.InverseTransformDirection(RB.velocity);
            locVel.z = speed;
            locVel.x = 0;
            locVel.y = Mathf.Clamp(locVel.y, maxFallSpeed, maxAscendSpeed);
            RB.velocity = transform.TransformDirection(locVel);

           
            isAscending = false;

            #endregion
        }
        else
        {
            RB.constraints = RigidbodyConstraints.FreezePosition;
            target = targ.position;
            target.y = targ.position.y + 1.8f;
            if (targ == start)
            {
                target.x = targ.localPosition.x + 0.2f;
                target.z = targ.localPosition.z + 0.2f;
                Debug.Log("HitStart");
            }
            else if (targ == quit)
            {
                target.x = targ.localPosition.x - 0.2f;
                target.z = targ.localPosition.z - 0.2f;
                Debug.Log("HitQuit");
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
            waitUntilAttack = 1f;
            Debug.Log("HitStart");
        }
    }

    public void StartTutorial()
    {
        // SceneManager.LoadScene("Tutorial");
    }

    public void LoadGame()

    {
        //SceneManager.LoadScene("MainScene");
        //Set the time back to 1:
        //Time.timeScale = 1;

    }

    public void QuitGame()
    {
        //Application.Quit();
    }
    public void LoadMenu()
    {

        //SceneManager.LoadScene("Credit");
    }


}