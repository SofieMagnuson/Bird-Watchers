using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human2 : MonoBehaviour
{
    public Player player;
    private HWaypoints waypts;
    public int wpointIndex;
    public bool isGoingBack, isPoopedOn;
    float speed, rotateTowardsWaypoint, setBoolToTrue;
    Color defaultColor;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        waypts = GameObject.FindGameObjectWithTag("waypoints").GetComponent<HWaypoints>();
        speed = 2f;
        rotateTowardsWaypoint = 3f;
        setBoolToTrue = 4f;
        defaultColor = GetComponent<Renderer>().material.color;
        anim.speed = 1.1f;
    }

    // Update is called once per frame
    void Update()
    {

        if (!player.reachedTarget && !isPoopedOn)
        {
            if (wpointIndex == 0)
            {
                isGoingBack = false;
            }

            Vector3 dir = waypts.wpoints2[wpointIndex].position - transform.position;
            dir.y = 0f;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotateTowardsWaypoint * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, waypts.wpoints2[wpointIndex].position, speed * Time.deltaTime);
            if (anim.GetBool("isWalking") == true)
            {
                anim.Play("walk");
            }
            if (anim.GetBool("isWalking") == false)
            {
                anim.SetBool("isWalking", true);
            }

            if (Vector3.Distance(transform.position, waypts.wpoints2[wpointIndex].position) < 0.1f)
            {
                if (wpointIndex < waypts.wpoints2.Length - 1)
                {
                    if (!isGoingBack)
                    {
                        wpointIndex++;
                    }
                    else { wpointIndex--; }
                }
                else
                {
                    wpointIndex -= 1;
                    isGoingBack = true;
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
