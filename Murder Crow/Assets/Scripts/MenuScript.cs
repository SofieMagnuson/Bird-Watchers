using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public Rigidbody RB;
    public float ascendSpeed, attackSpeed, waitUntilAttack, descendSpeed, lookAtTargetSpeed, speed, TStimer, maxVelocity;
    public bool isGrounded, isAscending, targetIsSet, reachedTarget, reachedBox;
    public LayerMask clickLayer;
    public Vector3 target;
    public Transform targ, StartBox, option, QuitBox, CreditBox;
    public float maxFallSpeed;
    [Range(0.0f, 10.0f)]
    public float maxAscendSpeed;
    public Camera cam;
    public Animator anim;
    public GameObject picture;
    private StartBox start = new StartBox();
    private QuitBox quit = new QuitBox();
    private CreditBox credit = new CreditBox();
    

    public void Start()
    {
        attackSpeed = 0.5f;
        waitUntilAttack = 1f;
        lookAtTargetSpeed = 1f;
        anim.Play("Flap");
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
                    if (hit.collider.gameObject.name == "StartBox")
                    {
                        targ = StartBox;
                    }
                    if (hit.collider.gameObject.name == "Option")
                    {
                       targ = option;
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
            target = targ.position;
            target.y = targ.position.y + 0.1f;
            if (targ == StartBox)
            {
                Debug.Log("HitStart");
                target.y = targ.position.y + 0.1f;
                target.x = targ.position.x;
                target.z = targ.position.z;
                Attack();
                FindObjectOfType<AudioManager>().Play("ButtonClick");
                if (reachedBox)
                {
                    start.start();
                }
            }
            if (targ == CreditBox)
            {
                Debug.Log("Credit");
                target.x = targ.localPosition.x + 1.0f;
                target.z = targ.localPosition.z - 0.2f;
                Debug.Log("HitCredit");
                Attack();
                credit.credit();
                FindObjectOfType<AudioManager>().Play("ButtonClick");
            }
            else if (targ == QuitBox)
            {
                target.x = targ.localPosition.x + 1.0f;
                target.z = targ.localPosition.z - 0.5f;
                Debug.Log("HitQuit");
                Attack();
                quit.quit();
                FindObjectOfType<AudioManager>().Play("ButtonClick");
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
            waitUntilAttack = 1f;
        }
    }


}