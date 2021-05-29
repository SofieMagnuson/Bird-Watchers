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
    public bool isPoopedOn, colliderTimer, started, movesToStartSpot, droppedGun;
    private Vector3 disToPlayer, target;
    public GameObject bullet, poopObj;
    public Transform startSpot;
    public Animator anim;
    public Renderer poop;
    public GameObject rifle;
    public MeshCollider rifleCol;


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
        FindObjectOfType<AudioManager>().Play("Drama");
    }

    void Update()
    {
        disToPlayer = player.transform.position - transform.position;

        if (startTimer <= 0)
        {
            started = true;
            startTimer = 4f;
            poopObj.SetActive(true);
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
                            if (!anim.GetBool("isShooting"))
                            {
                                anim.SetBool("aimFromIdle", true);
                            }
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
                    if (!droppedGun)
                    {
                        StartCoroutine(DropGun());
                        rifle.transform.parent = null;
                        rifle.GetComponent<Rigidbody>().useGravity = true;
                        rifleCol.enabled = true;
                        droppedGun = true;
                    }

                    if (player.reachedHunter)
                    {
                        if (!anim.GetBool("isAttacked"))
                        {
                            anim.SetBool("isAttacked", true);
                        }
                    }
                    else
                    {
                        if (anim.GetBool("isAttacked"))
                        {
                            anim.SetBool("isAttacked", false);
                        }
                    }
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
                player.doorAnim.SetBool("open", false);
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
            ReducePoopColor();
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
            FindObjectOfType<AudioManager>().Play("Eew");
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

    private void ReducePoopColor()
    {
        poop.material.color = new Color(poop.material.color.r, poop.material.color.g, poop.material.color.b, poop.material.color.a - 0.2f);
    }

    private IEnumerator DropGun()
    {
        anim.SetBool("isShooting", false);
        anim.SetBool("dropGun", true);
        yield return new WaitForSeconds(3.15f);
        anim.SetBool("dropGun", false);
    }
}
