using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human7 : MonoBehaviour
{
    public Player player;
    private HWaypoints waypts;
    public int wpointIndex;
    public float speed, waitBeforeMoving, rotateTowardsWaypoint, setBoolToTrue;
    private bool isPoopedOn;
    Color defaultColor;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        waypts = GameObject.FindGameObjectWithTag("waypoints").GetComponent<HWaypoints>();
        speed = 2f;
        rotateTowardsWaypoint = 1f;
        waitBeforeMoving = 0f;
        setBoolToTrue = 4f;
        defaultColor = GetComponent<Renderer>().material.color;

    }

    // Update is called once per frame
    void Update()
    {

        if (!player.reachedTarget && !isPoopedOn)
        {
            Vector3 dir = waypts.wpoints7[wpointIndex].position - transform.position;
            dir.y = 0f;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotateTowardsWaypoint * Time.deltaTime);
            if (waitBeforeMoving <= 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypts.wpoints7[wpointIndex].position, speed * Time.deltaTime);
                if (anim.GetBool("isWalking") == false)
                {
                    anim.SetBool("isWalking", true);
                }
            }
            else { waitBeforeMoving -= Time.deltaTime; }

            if (Vector3.Distance(transform.position, waypts.wpoints7[wpointIndex].position) < 0.1f)
            {
                if (wpointIndex < waypts.wpoints7.Length - 1)
                {
                    wpointIndex++;
                }
                else { wpointIndex = 0; }
                waitBeforeMoving = 3f;
                if (anim.GetBool("isWalking") == true)
                {
                    anim.SetBool("isWalking", false);
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
