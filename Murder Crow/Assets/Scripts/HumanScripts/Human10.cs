using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human10 : MonoBehaviour
{
    public Player player;
    public Human11 human11;
    private HWaypoints waypts;
    public int wpointIndex;
    public float speed, rotateTowardsWaypoint, setBoolToTrue;
    public bool isPoopedOn, isDead;
    public Animator anim;

    void Start()
    {
        waypts = GameObject.FindGameObjectWithTag("waypoints").GetComponent<HWaypoints>();
        speed = 1.61f;
        rotateTowardsWaypoint = 3.5f;
        setBoolToTrue = 4f;

    }

    void Update()
    {
        if (player.targ == player.targets[9] && player.pecks == player.peckAmountToKill)
        {
            isDead = true;
        }

        if (!player.reachedTarget && !isPoopedOn)
        {
            if (human11 != null)
            {
                if (!human11.isPoopedOn)
                {
                    Vector3 dir = waypts.wpoints10[wpointIndex].position - transform.position;
                    dir.y = 0f;
                    Quaternion lookRot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotateTowardsWaypoint * Time.deltaTime);

                    transform.position = Vector3.MoveTowards(transform.position, waypts.wpoints10[wpointIndex].position, speed * Time.deltaTime);
                    if (anim.GetBool("isWalking") == false)
                    {
                        anim.SetBool("isWalking", true);
                    }

                    if (Vector3.Distance(transform.position, waypts.wpoints10[wpointIndex].position) < 0.1f)
                    {
                        if (wpointIndex < waypts.wpoints10.Length - 1)
                        {
                            wpointIndex++;
                        }
                        else
                        {
                            wpointIndex = 0;
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
            }
            else
            {
                Vector3 dir = waypts.wpoints10[wpointIndex].position - transform.position;
                dir.y = 0f;
                Quaternion lookRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotateTowardsWaypoint * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, waypts.wpoints10[wpointIndex].position, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, waypts.wpoints10[wpointIndex].position) < 0.1f)
                {
                    if (wpointIndex < waypts.wpoints10.Length - 1)
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
