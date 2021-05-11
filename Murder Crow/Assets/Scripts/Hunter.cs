using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public Player player;
    public SphereCollider col1;
    public MeshCollider col2;
    private HWaypoints waypts;
    public int randomIndex, health;
    public float speed, rotateTowardsWaypoint, setBoolToTrue, waitBeforeMoving, shootingDistance, shootTimer, enableCol, stopTimer, startTimer;
    public bool isPoopedOn, colliderTimer, started;
    Color defaultColor;
    private Vector3 disToPlayer, target;
    public GameObject bullet;

    void Start()
    {
        waypts = GameObject.FindGameObjectWithTag("waypoints").GetComponent<HWaypoints>();
        speed = 1.2f;
        rotateTowardsWaypoint = 0.5f;
        setBoolToTrue = 4f;
        defaultColor = GetComponent<Renderer>().material.color;
        randomIndex = Random.Range(0, waypts.wpointsH.Length);
        waitBeforeMoving = 4;
        startTimer = 7f;
        shootingDistance = 18f;
        enableCol = 1.5f;
        shootTimer = 5f;
        health = 5;
    }

    void Update()
    {
        disToPlayer = player.transform.position - transform.position;

        if (player.mouseOnTarget)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = defaultColor;
        }
        if (startTimer <= 0)
        {
            started = true;
        }

        if (started)
        {
            if (health > 0)
            {
                if (disToPlayer.magnitude > shootingDistance)
                {
                    Vector3 dir = waypts.wpointsH[randomIndex].position - transform.position;
                    dir.y = 0f;
                    Quaternion lookRot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotateTowardsWaypoint * Time.deltaTime);
                    if (waitBeforeMoving <= 0)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, waypts.wpointsH[randomIndex].position, speed * Time.deltaTime);
                    }
                    else { waitBeforeMoving -= Time.deltaTime; }

                    if (Vector3.Distance(transform.position, waypts.wpointsH[randomIndex].position) < 0.1f)
                    {
                        randomIndex = Random.Range(0, waypts.wpointsH.Length);
                        waitBeforeMoving = 4f;
                    }
                }
                else if (disToPlayer.magnitude <= shootingDistance)
                {
                    transform.LookAt(player.transform.position);
                    Vector3 eulerAngles = transform.rotation.eulerAngles;
                    eulerAngles.x = 0;
                    eulerAngles.z = 0;
                    transform.rotation = Quaternion.Euler(eulerAngles);

                    if (shootTimer <= 0)
                    {
                        Shoot();
                        shootTimer = 5f;
                    }
                    else if (shootTimer > 0)
                    {
                        shootTimer -= Time.deltaTime;
                    }
                }
            }
            else
            {
                this.gameObject.layer = 6;
            }
        }
        else
        {
            startTimer -= Time.deltaTime;
        }

        if (enableCol <= 0)
        {
            col1.enabled = true;
            col2.enabled = true;
            colliderTimer = false;
            enableCol = 1.5f;
        }
        if (colliderTimer)
        {
            enableCol -= Time.deltaTime;
        }
        if (isPoopedOn)
        {
            col1.enabled = false;
            col2.enabled = false;
            health -= 1;
            colliderTimer = true;
            isPoopedOn = false;
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "poop")
        {
            isPoopedOn = true;
        }
    }

    public GameObject SpawnBullet(string prefab, Vector3 pos)
    {
        GameObject obj = Resources.Load<GameObject>(prefab);
        GameObject instance = GameObject.Instantiate(obj);
        instance.transform.position = pos;

        return instance;
    }

    private void Shoot()
    {
        target = player.transform.position + (player.transform.forward * 1.2f);
        Vector3 shootDir = target - transform.position;
        shootDir.Normalize();
        bullet = SpawnBullet("Prefabs/bullet", new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z + 0.3f) + shootDir);
        bullet.GetComponent<Rigidbody>().velocity = shootDir * 10;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(target, new Vector3(0.2f, 0.2f, 0.2f));
    }
}
