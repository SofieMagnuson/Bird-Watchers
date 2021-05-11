using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human3 : MonoBehaviour
{
    public Player player;
    private HWaypoints waypts;
    public int wpointIndex;
    public float speed, rotateTowardsWaypoint, setBoolToTrue;
    private bool isPoopedOn;
    Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        waypts = GameObject.FindGameObjectWithTag("waypoints").GetComponent<HWaypoints>();
        speed = 2f;
        rotateTowardsWaypoint = 3.5f;
        setBoolToTrue = 4f;
        defaultColor = GetComponent<Renderer>().material.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (player.mouseOnTarget)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = defaultColor;
        }

        if (!player.reachedTarget && !isPoopedOn)
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