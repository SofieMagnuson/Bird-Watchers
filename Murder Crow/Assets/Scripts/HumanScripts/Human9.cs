using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human9 : MonoBehaviour
{
    public Player player;
    private HWaypoints waypts;
    private int wpointIndex;
    public float speed, rotateTowardsWaypoint, setBoolToTrue;
    private bool isPoopedOn, reachedEnd;
    public Animator anim;

    void Start()
    {
        waypts = GameObject.FindGameObjectWithTag("waypoints").GetComponent<HWaypoints>();
        speed = 1.5f;
        rotateTowardsWaypoint = 3.5f;
        setBoolToTrue = 4f;
    }

    void Update()
    {
        if (!player.reachedTarget && !isPoopedOn)
        {
            if (!reachedEnd)
            {
                if (anim.GetBool("isAttacked"))
                {
                    anim.SetBool("isAttacked", false);
                }
                Vector3 dir = waypts.wpoints9[wpointIndex].position - transform.position;
                dir.y = 0f;
                Quaternion lookRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotateTowardsWaypoint * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, waypts.wpoints9[wpointIndex].position, speed * Time.deltaTime);
                if (anim.GetBool("isWalking") == false)
                {
                    anim.SetBool("isWalking", true);
                }
            }
            else
            {
                transform.position = waypts.wpoints9[wpointIndex].position;
                reachedEnd = false;
            }

            if (Vector3.Distance(transform.position, waypts.wpoints9[wpointIndex].position) < 0.1f)
            {
                if (wpointIndex < waypts.wpoints9.Length - 1)
                {
                    wpointIndex++;
                }
                else
                {
                    reachedEnd = true;
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
            if (!anim.GetBool("isAttacked"))
            {
                anim.SetBool("isAttacked", true);
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
