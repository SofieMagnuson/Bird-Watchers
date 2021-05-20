using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human11 : MonoBehaviour
{
    public Player player;
    public Human10 human10;
    private HWaypoints waypts;
    public int wpointIndex;
    public float speed, rotateTowardsWaypoint, setBoolToTrue;
    public bool isPoopedOn;
    private Vector3 disToH10;
    Color defaultColor;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        waypts = GameObject.FindGameObjectWithTag("waypoints").GetComponent<HWaypoints>();
        speed = 1.5f;
        rotateTowardsWaypoint = 3.5f;
        setBoolToTrue = 4f;
        defaultColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

        if (!human10.isDead)
        {
            disToH10 = human10.transform.position - transform.position; 
        }

        if (!player.reachedTarget && !isPoopedOn)
        {
            if (!human10.isDead)
            {
                if (disToH10.magnitude < 2.5f && !human10.isPoopedOn)
                {
                    Vector3 dir = waypts.wpoints11[wpointIndex].position - transform.position;
                    dir.y = 0f;
                    Quaternion lookRot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotateTowardsWaypoint * Time.deltaTime);

                    transform.position = Vector3.MoveTowards(transform.position, waypts.wpoints11[wpointIndex].position, speed * Time.deltaTime);
                    if (anim.GetBool("isWalking") == false)
                    {
                        anim.SetBool("isWalking", true);
                    }

                    if (Vector3.Distance(transform.position, waypts.wpoints11[wpointIndex].position) < 0.1f)
                    {
                        if (wpointIndex < waypts.wpoints11.Length - 1)
                        {
                            wpointIndex++;
                        }
                        else
                        {
                            wpointIndex = 0;
                        }
                    }
                }
                else if (disToH10.magnitude > 2.5f && human10.isPoopedOn)
                {
                    if (anim.GetBool("isWalking") == true)
                    {
                        anim.SetBool("isWalking", false);
                    }
                }
            }
            else
            {
                Vector3 dir = waypts.wpoints11[wpointIndex].position - transform.position;
                dir.y = 0f;
                Quaternion lookRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotateTowardsWaypoint * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, waypts.wpoints11[wpointIndex].position, speed * Time.deltaTime);
                if (anim.GetBool("isWalking") == false)
                {
                    anim.SetBool("isWalking", true);
                }


                if (Vector3.Distance(transform.position, waypts.wpoints11[wpointIndex].position) < 0.1f)
                {
                    if (wpointIndex < waypts.wpoints11.Length - 1)
                    {
                        wpointIndex++;
                    }
                    else
                    {
                        wpointIndex = 0;
                    }
                }
            }
        }
        else
        {
            if (anim.GetBool("isWalking") == true)
            {
                anim.SetBool("isWalking", false);
            }
        }
        if (setBoolToTrue <= 0)
        {
            isPoopedOn = false;
            setBoolToTrue = 4f;
        }
        if (isPoopedOn)
        {
            setBoolToTrue -= Time.deltaTime;
            if (anim.GetBool("isWalking") == true)
            {
                anim.SetBool("isWalking", false);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "poop")
        {
            isPoopedOn = true;
        }
    }
}
