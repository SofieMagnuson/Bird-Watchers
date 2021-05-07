using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody RB, skullRB;
    public Hunter hunter;
    public BoxCollider birdCol;
    public AchivementList achivementList;
    public SkinnedMeshRenderer birdMesh;
    public int health, pecks, peckAmountToKill, points, poops, poopAmount, caw, cawAmount, theChoosen1, theChoosen2, theChoosen3;
    public float speed, sprintspeed, normalspeed, ascendSpeed, turnSpeed, attackSpeed, waitUntilAttack, descendSpeed, lookAtTargetSpeed, maxVelocity, waitUntilMoving, maxHeight, maxTilt, tiltSpeed;
    public float tiltZ, tiltX, waitUntilInvinsable, invinsableTime, lowestHeight;
    public bool isAscending, targetIsSet, reachedTarget, reachedSkull, collided, inDropZone, invinsable, inUnder, mouseOnTarget, HumanZone, reachedHunter, hunterDead, hunterSkullDropped;
    public bool inWindZone = false;
    public LayerMask targetLayer, poopLayer;
    public Vector3 target, respawnPos, angles, skullPickup;
    public Transform targ, human1, human2, human3, target1, target2, target3, target4, target5, target6, target7, target8, target9, target10, target11, target12, target13, target14, target15;
    public Transform human4, human5, human6, human7, human8, human9, human10, human11, human12, human13, human14, human15;
    public Camera cam;
    public CameraMovement camScript;
    [Range(-10.0f, 0.0f)]
    public float maxFallSpeed;
    [Range(0.0f, 10.0f)]
    public float maxAscendSpeed, rotZ;
    public Animator anim;
    public GameObject skull, hunterSkull, WindZone, feather1, feather2, feather3, skull1, skull2, skull3, skull4, skull5, poop, choosen1, choosen2, choosen3, choosen4, choosen5;
    public GameObject choosen6, choosen7, choosen8, choosen9, choosen10, choosen11, choosen12, choosen13, choosen14, choosen15;
    private Color objectColor;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        normalspeed = 3f;
        sprintspeed = 6f;
        health = 3;
        respawnPos = new Vector3(-1.7f, 14.6f, -655.4f);
        ascendSpeed = 0.8f;
        descendSpeed = -2f;
        turnSpeed = 2.3f;
        attackSpeed = 0.5f;
        waitUntilAttack = 2f;
        waitUntilMoving = 2f;
        waitUntilInvinsable = 0.5f;
        lookAtTargetSpeed = 2f;
        invinsableTime = 5f;
        maxVelocity = 2f;
        maxHeight = 25f;
        lowestHeight = 9f;
        pecks = 0;
        peckAmountToKill = 10;
        poops = 0;
        poopAmount = 50;
        caw = 0;
        cawAmount = 3;
        tiltZ = 0;
        tiltX = 0;
        maxTilt = 20;
        tiltSpeed = 30;
        skullPickup = new Vector3(0, -0.206f, 0);
        RB = GetComponent<Rigidbody>();
        skull1.gameObject.SetActive(false);
        skull2.gameObject.SetActive(false);
        skull3.gameObject.SetActive(false);
        skull4.gameObject.SetActive(false);
        skull5.gameObject.SetActive(false);
        achivementList = GameObject.Find("AchivementList").GetComponent<AchivementList>();
        theChoosen1 = Random.Range(1, 5);
        theChoosen2 = Random.Range(5, 9);
        theChoosen3 = Random.Range(9, 14);
        Choose();

    }

    // Update is called once per frame
    void Update()
    {
        //Choose();

        //Cursor.lockState = CursorLockMode.Confined;
      
        if (transform.position.y >= maxHeight && Input.GetKey(KeyCode.W))
        {
            maxAscendSpeed = 0;
        }
        else
        {
            maxAscendSpeed = 3.45f;
        }
        if (!targetIsSet && !reachedSkull)
        {
            if (transform.position.y <= lowestHeight)
            {
                if (!Input.GetKey(KeyCode.W))
                {
                    RB.constraints = RigidbodyConstraints.FreezePositionY;
                }
                else
                {
                    RB.constraints = RigidbodyConstraints.None;
                }
            }
        }

        if (hunterSkullDropped)
        {
            Win();
        }

        #region set target
        if (!targetIsSet && !reachedTarget && !reachedHunter && !reachedSkull)
        {
            Vector3 mousePos = -Vector3.one;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10f, targetLayer))
            {
                mouseOnTarget = true;
            }
            else
            {
                mouseOnTarget = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                //Vector3 mousePos = -Vector3.one;

                //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                //RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 10f, targetLayer))
                {
                    mousePos = hit.point;
                    if (hit.collider.gameObject.name == "Human")
                    {
                        targ = target1;
                        camScript.attackTarget = camScript.attackTarget1;
                        mouseOnTarget = false;
                        choosen1.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human2")
                    {
                        targ = target2;
                        camScript.attackTarget = camScript.attackTarget2;
                        mouseOnTarget = false;
                        choosen2.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human3")
                    {
                        targ = target3;
                        camScript.attackTarget = camScript.attackTarget3;
                        mouseOnTarget = false;
                        choosen3.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human4")
                    {
                        targ = target4;
                        camScript.attackTarget = camScript.attackTarget4;
                        choosen4.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human5")
                    {
                        targ = target5;
                        camScript.attackTarget = camScript.attackTarget5;
                        choosen5.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human6")
                    {
                        targ = target6;
                        camScript.attackTarget = camScript.attackTarget6;
                        choosen6.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human7")
                    {
                        targ = target7;
                        camScript.attackTarget = camScript.attackTarget7;
                        mouseOnTarget = false;
                        choosen7.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human8")
                    {
                        targ = target8;
                        camScript.attackTarget = camScript.attackTarget8;
                        choosen8.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human9")
                    {
                        targ = target9;
                        camScript.attackTarget = camScript.attackTarget9;
                        mouseOnTarget = false;
                        choosen9.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human10")
                    {
                        targ = target10;
                        camScript.attackTarget = camScript.attackTarget10;
                        mouseOnTarget = false;
                        choosen10.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human11")
                    {
                        targ = target11;
                        camScript.attackTarget = camScript.attackTarget11;
                        mouseOnTarget = false;
                        choosen11.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human12")
                    {
                        targ = target12;
                        camScript.attackTarget = camScript.attackTarget12;
                        mouseOnTarget = false;
                        choosen12.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human13")
                    {
                        targ = target13;
                        camScript.attackTarget = camScript.attackTarget13;
                        choosen13.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Hunter")
                    {
                        targ = target14;
                        camScript.attackTarget = camScript.attackTarget14;
                    }
                    else if (hit.collider.gameObject.name == "Human15")
                    {
                        targ = target15;
                        camScript.attackTarget = camScript.attackTarget15;
                        choosen15.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject == skull)
                    {
                        targ = skull.transform;
                    }
                    else if (hit.collider.gameObject == hunterSkull)
                    {
                        targ = hunterSkull.transform;
                    }
                    targetIsSet = true;
                }
            }
        }
        #endregion

        #region attacking

        if (pecks == peckAmountToKill)
        {
            if (targ == target1)
            {
                skull = SpawnObject("Prefabs/skull", new Vector3(human1.position.x, human1.position.y + 1f, human1.position.z));
                human1.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target2)
            {
                skull = SpawnObject("Prefabs/skull", new Vector3(human2.position.x, human2.position.y + 1f, human2.position.z));
                human2.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target3)
            {
                skull = SpawnObject("Prefabs/skull", new Vector3(human3.position.x, human3.position.y + 1f, human3.position.z));
                human3.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target4)
            {
                skull = SpawnObject("Prefabs/skull", new Vector3(human4.position.x, human4.position.y + 1f, human4.position.z));
                human4.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target5)
            {
                skull = SpawnObject("Prefabs/skull", new Vector3(human5.position.x, human5.position.y + 1f, human5.position.z));
                human5.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target6)
            {
                skull = SpawnObject("Prefabs/skull", new Vector3(human6.position.x, human6.position.y + 1f, human6.position.z));
                human6.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target7)
            {
                skull = SpawnObject("Prefabs/skull", new Vector3(human7.position.x, human7.position.y + 1f, human7.position.z));
                human7.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target8)
            {
                skull = SpawnObject("Prefabs/skull", new Vector3(human8.position.x, human8.position.y + 1f, human8.position.z));
                human9.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target9)
            {
                skull = SpawnObject("Prefabs/skull", new Vector3(human9.position.x, human9.position.y + 1f, human9.position.z));
                human9.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target10)
            {
                skull = SpawnObject("Prefabs/skull", new Vector3(human10.position.x, human10.position.y + 1f, human10.position.z));
                human10.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target11)
            {
                skull = SpawnObject("Prefabs/skull", new Vector3(human11.position.x, human11.position.y + 1f, human11.position.z));
                human11.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target12)
            {
                Debug.Log("hej");
                skull = SpawnObject("Prefabs/skull", new Vector3(human12.position.x, human12.position.y + 1f, human12.position.z));
                human12.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target13)
            {
                skull = SpawnObject("Prefabs/skull", new Vector3(human13.position.x, human13.position.y + 1f, human13.position.z));
                human13.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target14)
            {
                hunterDead = true;
                hunterSkull = SpawnObject("Prefabs/skull", new Vector3(human14.position.x, human14.position.y + 1f, human14.position.z));
                human14.gameObject.SetActive(false);
                reachedHunter = false;
                targ = null;
            }
            else if (targ == target15)
            {
                Debug.Log("Pink Skirt");
                skull = SpawnObject("Prefabs/skull", new Vector3(human15.position.x, human15.position.y + 1f, human15.position.z));
                human15.gameObject.SetActive(false);
                achivementList.ListThreeThree();
                targ = null;
            }
            reachedTarget = false;
            waitUntilMoving -= Time.deltaTime;
            if (waitUntilMoving <= 0)
            {
                RB.constraints = RigidbodyConstraints.None;
                waitUntilMoving = 2f;
                pecks = 0;
            }
        }
        if (reachedTarget || reachedHunter)
        {
            Vector3 dir = camScript.attackTarget.position - transform.position;
            dir.y = 0f;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, lookAtTargetSpeed * Time.deltaTime);
        }

        if ((reachedTarget || reachedHunter) && Input.GetMouseButtonDown(0))
        {
            if (pecks < peckAmountToKill)
            {
                pecks += 1;
            }
              
        }
        #endregion

        #region pooping
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 mousePos = -Vector3.one;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, poopLayer))
            {
                mousePos = hit.point;

                Vector3 poopDir = mousePos - transform.position;
                poopDir.Normalize();
                poop = SpawnObject("Prefabs/poop", new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z) + poopDir);
                poop.GetComponent<Rigidbody>().velocity = poopDir * 10;

                if (poops < poopAmount)
                {
                    poops += 1;

                }
                if (poops == poopAmount)
                {
                    achivementList.ListTwo();
                }
     
            }
        }
        #endregion

        if ((reachedTarget || reachedHunter) && Input.GetKey(KeyCode.W))
        {
            RB.constraints = RigidbodyConstraints.None;
            isAscending = true;
            RB.AddForce(new Vector3(0, ascendSpeed * 2f, 0), ForceMode.Impulse);
        }
        if (invinsableTime <= 0)
        {
            birdCol.enabled = true;
            invinsable = false;
            birdMesh.material.color = Color.gray;
            invinsableTime = 5f;
        }
        if (invinsable)
        {
            birdMesh.material.color = Color.red;
            invinsableTime -= Time.deltaTime;
        }
        if (waitUntilInvinsable <= 0)
        {
            health -= 1;
            birdCol.enabled = false;
            RB.constraints = RigidbodyConstraints.None;
            collided = false;
            invinsable = true;
            waitUntilInvinsable = 1f;
        }
        if (collided)
        {
            waitUntilInvinsable -= Time.deltaTime;
        }
        switch (health)
        {
            case 2:
                feather3.gameObject.SetActive(false);
                break;
            case 1:
                feather2.gameObject.SetActive(false);
                achivementList.ListThreeThreeThree();
                break;
            case 0:
                feather1.gameObject.SetActive(false);
                Lose();
                break;
        }

        #region animations
        if (anim.GetBool("isFlyingUp") == true)
        {
            anim.Play("Flap");
        }
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isFlyingUp", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("isFlyingUp", false);
        }
        #endregion


        #region Caw

        if (Input.GetKey(KeyCode.Q))
        {
            FindObjectOfType<AudioManager>().Play("Caw");

        }

        if (HumanZone && Input.GetKey(KeyCode.Q))
        {
            HumanZone = true;
            if (caw < cawAmount)
            {
                caw += 1;
            }
            if (caw == cawAmount)
            {
                achivementList.ListTwoTwoTwo();
            }
        }
        #endregion




    }

    private void FixedUpdate()
    {

        if (!targetIsSet)
        {

            #region movement

            Vector3 locVel = transform.InverseTransformDirection(RB.velocity);
            locVel.z = speed;
            locVel.x = 0;
            locVel.y = Mathf.Clamp(locVel.y, maxFallSpeed, maxAscendSpeed);
            RB.velocity = transform.TransformDirection(locVel);

           
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                RB.angularVelocity = new Vector3(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                isAscending = true;
                RB.AddForce(new Vector3(0, ascendSpeed, 0), ForceMode.Impulse);
                if (transform.position.y < maxHeight - 2)
                {
                    tiltX = Mathf.Max(tiltX - 20 * Time.deltaTime, -maxTilt);
                }
                else
                {
                    tiltX = Mathf.Min(tiltX + tiltSpeed * 2 * Time.deltaTime, 0);
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (transform.position.y > lowestHeight)
                {
                    tiltX = Mathf.Min(tiltX + 20 * Time.deltaTime, maxTilt);
                    RB.AddForce(new Vector3(0, descendSpeed, 0), ForceMode.Impulse);
                }
                else
                {
                    Mathf.Max(tiltX - tiltSpeed * 2 * Time.deltaTime, 0);

                }
            }
            else if (tiltX != 0)
            {
                tiltX = tiltX < 0 ? Mathf.Min(tiltX + tiltSpeed * 2 * Time.deltaTime, 0) : Mathf.Max(tiltX - tiltSpeed * 2 * Time.deltaTime, 0);
            }
            //Sprintspeed
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = sprintspeed;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = normalspeed;
            }
            //sprintspeedstop
            
            if (!reachedTarget && !reachedHunter)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
                    tiltZ = Mathf.Min(tiltZ + tiltSpeed * Time.deltaTime, maxTilt);
                    RB.AddTorque(transform.up * turn, ForceMode.VelocityChange);
                    if (RB.angularVelocity.y <= -maxVelocity)
                    {
                        RB.angularVelocity = new Vector3(RB.angularVelocity.x, -maxVelocity, RB.angularVelocity.z);
                    }
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
                    tiltZ = Mathf.Max(tiltZ - tiltSpeed * Time.deltaTime, -maxTilt);
                    RB.AddTorque(transform.up * turn, ForceMode.VelocityChange);
                    if (RB.angularVelocity.y >= maxVelocity)
                    {
                        RB.angularVelocity = new Vector3(RB.angularVelocity.x, maxVelocity, RB.angularVelocity.z);
                    }
                }
                else if (tiltZ != 0)
                {
                    tiltZ = tiltZ < 0 ? Mathf.Min(tiltZ + tiltSpeed * 2 * Time.deltaTime, 0) : Mathf.Max(tiltZ - tiltSpeed * 2 * Time.deltaTime, 0);
                }

            }

            angles = new Vector3(tiltX, transform.eulerAngles.y, tiltZ);
            transform.rotation = Quaternion.Euler(angles);

            
            isAscending = false;
            #endregion

            #region pickUp

            if (reachedSkull)
            {
                if (Input.GetMouseButton(0))
                {
                    PickUpSkull();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    if (inDropZone && theChoosen1 == 1)
                    {
                        if (hunterSkull == null)
                        {
                            points += 1;
                        }
                        else
                        {
                            hunterSkullDropped = true;
                            hunterSkull.transform.parent = null;
                        }
                    }
                    if (skull != null)
                    {
                        skull.transform.parent = null;
                        skullRB.useGravity = true;
                        skull = null;
                    }
                    targ = null;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    RB.constraints = RigidbodyConstraints.None;
                }
            }
            switch (points)
            {
                case 1:
                    skull1.gameObject.SetActive(true);
                    if (!hunterDead)
                    {
                        hunter.gameObject.SetActive(true);
                    }
                    break;
                case 2:
                    skull1.gameObject.SetActive(true);
                    if (!hunterDead)
                    {
                        hunter.gameObject.SetActive(true);
                    }
                    break;
                case 3:
                    skull2.gameObject.SetActive(true);
                    break;
                case 4:
                    skull2.gameObject.SetActive(true);
                    break;
                case 5:
                    skull3.gameObject.SetActive(true);
                    break;
                case 6:
                    //skull1.gameObject.SetActive(true);
                    //skull2.gameObject.SetActive(true);
                    //skull3.gameObject.SetActive(true);
                    //Win();
                    break;
                //case 0:
                //    //skull1.gameObject.SetActive(true);
                //    //skull2.gameObject.SetActive(true);
                //    //skull3.gameObject.SetActive(true);
                //    Win();
                    //break;
            }

            if (inWindZone)
            {
                RB.AddForce(WindZone.GetComponent<WindArea>().direction * WindZone.GetComponent <WindArea>().strength);
            }


            if (inUnder)
            {
                achivementList.ListTwoTwo();
            }

            #endregion

        }
        else
        {
            RB.constraints = RigidbodyConstraints.FreezePosition;
            target = targ.position; 
            if (skull != null)
            {
                if (targ == skull.transform)
                {
                    target.y = targ.position.y + 0.26f;
                    target.x = targ.position.x;
                    target.z = targ.position.z;
                }
            }
            else if (hunterSkull != null)
            {
                if (targ == hunterSkull.transform)
                {
                    target.y = targ.position.y + 0.26f;
                    target.x = targ.position.x;
                    target.z = targ.position.z;
                }
            }
            Vector3 dir = target - transform.position;
            dir.y = 0f;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, lookAtTargetSpeed * Time.deltaTime);
            waitUntilAttack -= Time.deltaTime;
            if (waitUntilAttack <= 0)
            {
                Attack();
            }
        }
    }
    public void Choose()
    {
        if (theChoosen1 == 1)
        {
            choosen1.gameObject.SetActive(true);
            //human1 = target1;
        }
        if (theChoosen1 == 2)
        {
            choosen2.gameObject.SetActive(true);
            //human2 = target2;
        }
        if (theChoosen1 == 3)
        {
            choosen3.gameObject.SetActive(true);
            //human3 = target3;
        }
        if (theChoosen1 == 4)
        {
            choosen4.gameObject.SetActive(true);
            //human4 = target4;
        }
        if (theChoosen2 == 5)
        {
            choosen5.gameObject.SetActive(true);
            //human5 = target5;
        }
        if (theChoosen2 == 6)
        {
            choosen6.gameObject.SetActive(true);
            //human6 = target6;
        }
        if (theChoosen2 == 7)
        {
            choosen7.gameObject.SetActive(true);
            //human7 = target7;
        }
        if (theChoosen2 == 8)
        {
            choosen8.gameObject.SetActive(true);
            //human8 = target8;
        }
        if (theChoosen2 == 9)
        {
            choosen9.gameObject.SetActive(true);
            //human9 = target9;
        }
        if (theChoosen3 == 10)
        {
            choosen10.gameObject.SetActive(true);
            //human10 = target10;
        }
        if (theChoosen3 == 11)
        {
            choosen11.gameObject.SetActive(true);
            //human11 = target11;
        }
        if (theChoosen3 == 12)
        {
            choosen12.gameObject.SetActive(true);
            //human12 = target12;
        }
        if (theChoosen3 == 13)
        {
            choosen13.gameObject.SetActive(true);
            //human13 = target13;
        }
        //if (theChoosen3 == 14)
        // {
        //     choosen14.gameObject.SetActive(true);
        //     human14 = target14;
        // }
        if (theChoosen3 == 15)
        {
            choosen15.gameObject.SetActive(true);
            //human15 = target15;
        }
    }

    public void Attack()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(target.x, target.y, target.z);
        transform.position = Vector3.MoveTowards(startPos, endPos, attackSpeed);
        if (startPos == endPos)
        {
            targetIsSet = false;
            waitUntilAttack = 2f;
        }
    }

    public GameObject SpawnObject(string prefab, Vector3 pos)
    {
        GameObject obj = Resources.Load<GameObject>(prefab);
        GameObject instance = GameObject.Instantiate(obj);
        instance.transform.position = pos;

        return instance;
    }

    private void PickUpSkull()
    {
        if (skull != null)
        {
            if (targ == skull.transform)
            {
                skull.transform.parent = transform;
                skull.transform.localPosition = skullPickup;
                skullRB = skull.GetComponent<Rigidbody>();
                skullRB.useGravity = false;
            }
        }
        else if (hunterSkull != null)
        {
            if (targ == hunterSkull.transform)
            {
                hunterSkull.transform.parent = transform;
                hunterSkull.transform.localPosition = skullPickup;
                skullRB = hunterSkull.GetComponent<Rigidbody>();
                skullRB.useGravity = false;
            }
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "human")
        {
            reachedTarget = true;
        }
        if (col.gameObject.name == "Hunter")
        {
            reachedHunter = true;
        }
        if (col.gameObject.name == "skull(Clone)")
        {
            reachedSkull = true;
        }
        if (col.gameObject.name == "DropSkullArea")
        {
            inDropZone = true;
        }
        if (col.gameObject.tag == "WindArea")
        {
            WindZone = col.gameObject;
            inWindZone = true;
        }
        if (col.gameObject.tag == "under")
        {
            inUnder = true;
        }
        if (col.gameObject.tag == "HumanZone")
        {
            HumanZone = true;
        }
        
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "human")
        {
            reachedTarget = false;
        }
        if (col.gameObject.name == "Hunter")
        {
            reachedHunter = false;
        }
        if (col.gameObject.name == "skull(Clone)")
        {
            reachedSkull = false;
        }
        if (col.gameObject.name == "DropSkullArea")
        {
            inDropZone = false;
        }
        if (col.gameObject.tag == "WindArea")
        {
            WindZone = col.gameObject;
            inWindZone = false;
        }
        if (col.gameObject.tag == "under")
        {
            inUnder = false;
        }
        if (col.gameObject.tag == "HumanZone")
        {
            HumanZone = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "terrain")
        {
            FindObjectOfType<AudioManager>().Play("Collision");
        }

        if (col.gameObject.tag == "obstacles")
        {
            RB.constraints = RigidbodyConstraints.FreezeRotation;
            collided = true;
            FindObjectOfType<AudioManager>().Play("Collision");
        }

    }
    
    private void Lose()
    
    {
        SceneManager.LoadScene("Looose");
    
    }

    private void Win()
    {
        SceneManager.LoadScene("Win");
    }

}