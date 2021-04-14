using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human3 : MonoBehaviour
{
    public Player player;
    private HWaypoints waypts;
    public int wpointIndex;
    float speed, rotateTowardsWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        waypts = GameObject.FindGameObjectWithTag("waypoints").GetComponent<HWaypoints>();
        speed = 3f;
        rotateTowardsWaypoint = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.reachedTarget)
        {
            Vector3 dir = waypts.wpoints3[wpointIndex].position - transform.position;
            dir.y = 0f;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotateTowardsWaypoint * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, waypts.wpoints3[wpointIndex].position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, waypts.wpoints3[wpointIndex].position) < 0.1f)
            {
                if (wpointIndex < waypts.wpoints3.Length - 1)
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
}
