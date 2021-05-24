using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public Player player;
    public CapsuleCollider col;
    private HWaypoints waypts;
    public int randomIndex, health;
    public float speed, rotateTowardsWaypoint, setBoolToTrue, waitBeforeMoving, shootingDistance, shootTimer, enableCol, stopTimer, startTimer, sceneTimer, fromAimToShoot;
    public bool isPoopedOn, colliderTimer, started, movesToStartSpot;
    private Vector3 disToPlayer, target;
    public GameObject bullet;
    public Transform startSpot;
    public Animator anim;


    void Start()
    {
        waypts = GameObject.FindGameObjectWithTag("waypoints").GetComponent<HWaypoints>();
        speed = 1.2f;
        rotateTowardsWaypoint = 0.5f;
        setBoolToTrue = 4f;
        randomIndex = Random.Range(0, waypts.wpointsH.Length);
        waitBeforeMoving = 4;
        startTimer = 3f;
        sceneTimer = 4f;
        shootingDistance = 18f;
        enableCol = 1.5f;
        shootTimer = 5f;
        health = 5;
        fromAimToShoot = 2f;
        movesToStartSpot = true;
    }

    void Update()
    {
        disToPlayer = player.transform.position - transform.position;

        if (startTimer <= 0)
        {
            started = true;
            player.doorAnim.SetBool("open", false);
            startTimer = 4f;
        }
        if (sceneTimer <= 0)
        {
            if (movesToStartSpot)
            {
                movesToStartSpot = false;
            }
        }
        

        if (!movesToStartSpot)
        {
            if (started)
            {
                if (health > 0)
                {
                    if (disToPlayer.magnitude > shootingDistance)
                    {
                        if (fromAimToShoot != 2)
                        {
                            fromAimToShoot = 2f;
                        }
                        if (anim.GetBool("isShooting"))
                        {
                            anim.SetBool("isShooting", false);
                        }
                        Vector3 dir = waypts.wpointsH[randomIndex].position - transform.position;
                        dir.y = 0f;
                        Quaternion lookRot = Quaternion.LookRotation(dir);
                        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotateTowardsWaypoint * Time.deltaTime);
                        if (waitBeforeMoving <= 0)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, waypts.wpointsH[randomIndex].position, speed * Time.deltaTime);
                            
                            if (!anim.GetBool("isWalking"))
                            {
                                anim.SetBool("isWalking", true);
                            }
                        }
                        else
                        {
                            waitBeforeMoving -= Time.deltaTime;
                        }

                        if (Vector3.Distance(transform.position, waypts.wpointsH[randomIndex].position) < 0.1f)
                        {
                            randomIndex = Random.Range(0, waypts.wpointsH.Length);
                            waitBeforeMoving = 4f;
                            if (anim.GetBool("isWalking"))
                            {
                                anim.SetBool("isWalking", false);
                            }
                        }
                    }
                    else if (disToPlayer.magnitude <= shootingDistance)
                    {
                        fromAimToShoot -= Time.deltaTime;
                        if (anim.GetBool("isWalking"))
                        {
                            anim.SetBool("aimFromWalk", true);
                            anim.SetBool("isWalking", false);
                        }
                        else
                        {
                            anim.SetBool("aimFromIdle", true);
                        }
                        if (fromAimToShoot <= 0)
                        {
                            if (!anim.GetBool("isShooting"))
                            {
                                anim.SetBool("aimFromWalk", false);
                                anim.SetBool("aimFromIdle", false);
                                anim.SetBool("isShooting", true);
                            }
                        }
                        transform.LookAt(player.transform.position);
                        Vector3 eulerAngles = transform.rotation.eulerAngles;
                        eulerAngles.x = 0;
                        eulerAngles.z = 0;
                        transform.rotation = Quaternion.Euler(eulerAngles);

                        if (shootTimer <= 0)
                        {
                            Shoot();
                            FindObjectOfType<AudioManager>().Play("Gun");
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
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startSpot.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startSpot.position) < speed * Time.deltaTime)
            {
                transform.position = startSpot.position;
                if (anim.GetBool("isWalking"))
                {
                    anim.SetBool("isWalking", false);
                }
            }
            else
            {
                if (!anim.GetBool("isWalking"))
                {
                    anim.SetBool("isWalking", true);
                }
            }
            sceneTimer -= Time.deltaTime;
        }

        if (enableCol <= 0)
        {
            col.enabled = true;
            colliderTimer = false;
            enableCol = 1.5f;
        }
        if (colliderTimer)
        {
            enableCol -= Time.deltaTime;
        }
        if (isPoopedOn)
        {
            col.enabled = false;
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
}
