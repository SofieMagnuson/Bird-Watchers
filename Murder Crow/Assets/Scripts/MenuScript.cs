using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Rigidbody RB;
    public float ascendSpeed, attackSpeed, waitUntilAttack, waitUntilMoving, descendSpeed, lookAtTargetSpeed, speed, TStimer, maxVelocity;
    public bool isGrounded, isAscending, targetIsSet, reachedTarget, reachedBox;
    public LayerMask clickLayer;
    public Vector3 target;
    public Transform targ, StartBox, OptionBox, QuitBox, CreditBox;
    public float maxFallSpeed;
    [Range(0.0f, 10.0f)]
    public float maxAscendSpeed;
    public Camera cam;
    public Animator anim;
    public GameObject picture;
    private StartBox start = new StartBox();
    private QuitBox quit = new QuitBox();
    private CreditBox credit = new CreditBox();
    private OptionBox options = new OptionBox();



    public void Start()
    {
        attackSpeed = 0.5f;
        waitUntilAttack = 5f;
        lookAtTargetSpeed = 3f;
        waitUntilMoving = 2f;
        //anim.Play("Flap");
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
                if (Physics.Raycast(ray, out hit, 10f, clickLayer))
                {
                    mousePos = hit.point;
                    if (hit.collider.gameObject.name == "StartBox")
                    {
                        targ = StartBox;
                    }
                    if (hit.collider.gameObject.name == "OptionBox")
                    {
                       targ = OptionBox;
                    }
                    if (hit.collider.gameObject.name == "QuitBox")
                    {
                        targ = QuitBox;
                    }
                    if (hit.collider.gameObject.name == "CreditBox")
                    {
                        targ = CreditBox;
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
            target.x = targ.position.x + 0.1f;           
            target.y = targ.position.y + 0.1f;
            if (targ == StartBox)
            {
                Debug.Log("HitStart");
                target.y = targ.position.y - 0.2f;
                target.x = targ.position.x - 0.5f;
                target.z = targ.position.z;
                Attack();
                FindObjectOfType<AudioManager>().Play("ButtonClick");
                if (reachedBox)
                {
                    start.start();
                }
            }
            if (targ == OptionBox)
            {
                Debug.Log("HitOptions");
                target.y = targ.position.y - 0.2f;
                target.x = targ.position.x - 0.5f;
                target.z = targ.position.z;
                Attack();
                FindObjectOfType<AudioManager>().Play("ButtonClick");
                if (reachedBox)
                {
                    options.options();
                }
            }
            if (targ == CreditBox)
            {
                Debug.Log("Credit");
                target.y = targ.position.y - 0.4f;
                target.x = targ.position.x - 0.2f;
                target.z = targ.position.z;
                Debug.Log("HitCredit");
                Attack();
                FindObjectOfType<AudioManager>().Play("ButtonClick");
                if (reachedBox)
                {
                    credit.credit();
                }
             
            }
            else if (targ == QuitBox)
            {
                target.y = targ.position.y - 0.4f;
                target.x = targ.position.x - 0.5f;
                target.z = targ.position.z - 0.5f;
                Debug.Log("HitQuit");
                Attack();
                FindObjectOfType<AudioManager>().Play("ButtonClick");
                if (reachedBox)
                {
                    quit.quit();
                }
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

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "picture")
        {
            reachedBox = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "picture")
        {
            reachedBox = false;
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
            waitUntilAttack = 5f;
        }
    }


}