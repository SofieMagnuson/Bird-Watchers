using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    private HWaypoints waypts;
    float speed, waitBeforeMoving;

    // Start is called before the first frame update
    void Start()
    {
        waypts = GameObject.FindGameObjectWithTag("waypoints").GetComponent<HWaypoints>();
        speed = 0.6f;
        waitBeforeMoving = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypts.wpoints[waypts.wpointIndex].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypts.wpoints[waypts.wpointIndex].position) < 0.1f)
        {
            if (waitBeforeMoving <= 0)
            {
                if (waypts.wpointIndex < waypts.wpoints.Length - 1)
                {
                    waypts.wpointIndex++;
                }
                else { waypts.wpointIndex = 0; }
                //waitBeforeMoving = startwaitTime;
            }
            //else { waitTime -= Time.deltaTime; }
        }

        
    }
}
