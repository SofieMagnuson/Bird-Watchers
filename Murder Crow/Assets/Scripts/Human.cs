using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public Player player;
    private HWaypoints waypts;
    public int wpointIndex;
    float speed, waitBeforeMoving, rotateTowardsWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        waypts = GameObject.FindGameObjectWithTag("waypoints").GetComponent<HWaypoints>();
        speed = 2f;
        rotateTowardsWaypoint = 1f;
        waitBeforeMoving = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.reachedTarget)
        {
            Vector3 dir = waypts.wpoints[wpointIndex].position - transform.position;
            dir.y = 0f;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotateTowardsWaypoint * Time.deltaTime);
            if (waitBeforeMoving <= 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypts.wpoints[wpointIndex].position, speed * Time.deltaTime);
            }
            else { waitBeforeMoving -= Time.deltaTime; }

            if (Vector3.Distance(transform.position, waypts.wpoints[wpointIndex].position) < 0.1f)
            {
                if (wpointIndex < waypts.wpoints.Length - 1)
                {
                    wpointIndex++;
                }
                else { wpointIndex = 0; }
                waitBeforeMoving = 3f;
            }
        }
    }
}
