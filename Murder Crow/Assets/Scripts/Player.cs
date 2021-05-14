using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody RB, skullRB;
    public Hunter hunter;
    public DoorScript door;
    public BoxCollider birdCol;
    public AchivementList achivementList;
    public SkinnedMeshRenderer birdMesh;
    public int health, pecks, peckAmountToKill, points, pointsToWin, poops, poopAmount, caw, cawAmount, cawOne, randomkill, randomkillAmount, randomkillOne, theChoosen1, theChoosen2, theChoosen3, dropCount;
    public float speed, sprintspeed, normalspeed, ascendSpeed, turnSpeed, attackSpeed, waitUntilAttack, descendSpeed, lookAtTargetSpeed, maxVelocity, waitUntilMoving, maxHeight, maxTilt, tiltSpeed;
    public float tiltZ, tiltX, waitUntilInvinsable, invinsableTime, lowestHeight, rendererOnOff, setBoolToFalse, cawTimer;
    public bool isAscending, targetIsSet, reachedTarget, reachedSkull, reachedSkullNoPoint, collided, inDropZone, invinsable, inUnder, mouseOnTarget, HumanZone, Luft, reachedHunter, hunterDead, hunterSkullDropped, tutorialMode;
    public bool inWindZone = false;
    public bool turningLeft, turningRight, droppedSkull, showedHunter, cawed;
    public LayerMask targetLayer, poopLayer;
    public Vector3 target, respawnPos, angles, skullPickup;
    public Transform targ, human1, human2, human3, target1, target2, target3, target4, target5, target6, target7, target8, target9, target10, target11, target12, target13, target14, target15;
    public Transform human4, human5, human6, human7, human8, human9, human10, human11, human12, human13, human14, human15, dropPos1, dropPos2, dropPos3, dropPos4, dropPos5, dropPos6, dropPos7, dropPos8,
            dropPos9, dropPos10, dropPos11, dropPos12, dropPos13, dropPos14, dropPos15;
    public Transform RP, rotatePoint1, rotatePoint2, rotatePoint3, rotatePoint4, rotatePoint5, rotatePoint6, rotatePoint7, rotatePoint8, rotatePoint9, rotatePoint10, rotatePoint11, rotatePoint12, rotatePoint13, rotatePoint14, rotatePoint15;
    public Camera cam;
    public CameraMovement camScript;
    [Range(-10.0f, 0.0f)]
    public float maxFallSpeed;
    [Range(0.0f, 10.0f)]
    public float maxAscendSpeed, rotZ;
    public Animator anim, doorAnim;
    public GameObject skull, skullNoPoint, hunterSkull, WindZone, feather1, feather2, feather3, skull1, skull2, skull3, skull4, skull5, skullhunter, poop, choosen1, choosen2, choosen3, choosen4, choosen5;
    public GameObject choosen6, choosen7, choosen8, choosen9, choosen10, choosen11, choosen12, choosen13, choosen14, choosen15, picture1, picture2, picture3, picture4, picture5, picture6, picture7, picture8, picture9, picture10,
        picture11, picture12, picture13, picture15, chosenSkull, tutorialText;
    private Color objectColor;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        tutorialMode = true;
        points = 0;
        pointsToWin = 3;
        randomkill = 0;
        randomkillOne = 1;
        randomkillAmount = 3;
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
        rendererOnOff = 0.3f;
        setBoolToFalse = 8f;
        cawTimer = 2f;
        pecks = 0;
        peckAmountToKill = 10;
        poops = 0;
        poopAmount = 40;
        caw = 0;
        dropCount = 0;
        cawOne = 1;
        cawAmount = 2;
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
        skullhunter.gameObject.SetActive(false);
        //achivementList = GameObject.Find("AchivementList").GetComponent<AchivementList>();

        Choose();
    }

    // Update is called once per frame
    void Update()
    {
        //Cursor.lockState = CursorLockMode.Confined;

        #region input

        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    turningLeft = false;
        //}
        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    turningRight = false;
        //}
        //if (!reachedTarget && !reachedHunter)
        //{
        //    if (Input.GetKey(KeyCode.A))
        //    {
        //        turningLeft = true;
        //    }
        //    else if (Input.GetKey(KeyCode.D))
        //    {
        //        turningRight = true;
        //    }
        //    else if (tiltZ != 0)
        //    {
        //        tiltZ = tiltZ < 0 ? Mathf.Min(tiltZ + tiltSpeed * 2 * Time.deltaTime, 0) : Mathf.Max(tiltZ - tiltSpeed * 2 * Time.deltaTime, 0);
        //    }

        //}

        //angles = new Vector3(tiltX, transform.eulerAngles.y, tiltZ);
        //transform.rotation = Quaternion.Euler(angles);

        #endregion

        if (tutorialMode)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.up, -45 * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up, 45 * Time.deltaTime);
            }
        }

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
            skullhunter.gameObject.SetActive(true);
            Win();
            
        }

        if (camScript.showHunter)
        {
            RB.constraints = RigidbodyConstraints.FreezeAll;
        }
        else if (camScript.showingTime <= 0 && !showedHunter)
        {
            RB.constraints = RigidbodyConstraints.None;
            showedHunter = true;
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
                if (Physics.Raycast(ray, out hit, 10f, targetLayer))
                {
                    mousePos = hit.point;
                    if (hit.collider.gameObject.name == "Human")
                    {
                        targ = target1;
                        camScript.attackTarget = camScript.attackTarget1;
                        RP = rotatePoint1;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        mouseOnTarget = false;
                        choosen1.gameObject.SetActive(false);
                        FindObjectOfType<AudioManager>().Play("Flapping");
                    }
                    else if (hit.collider.gameObject.name == "Human2")
                    {
                        targ = target2;
                        camScript.attackTarget = camScript.attackTarget2;
                        RP = rotatePoint2;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        mouseOnTarget = false;
                        choosen2.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human3")
                    {
                        targ = target3;
                        camScript.attackTarget = camScript.attackTarget3;
                        RP = rotatePoint3;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        mouseOnTarget = false;
                        choosen3.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human4")
                    {
                        targ = target4;
                        camScript.attackTarget = camScript.attackTarget4;
                        RP = rotatePoint4;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        choosen4.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human5")
                    {
                        targ = target5;
                        camScript.attackTarget = camScript.attackTarget5;
                        RP = rotatePoint5;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        choosen5.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human6")
                    {
                        targ = target6;
                        camScript.attackTarget = camScript.attackTarget6;
                        RP = rotatePoint6;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        choosen6.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human7")
                    {
                        targ = target7;
                        camScript.attackTarget = camScript.attackTarget7;
                        RP = rotatePoint7;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        mouseOnTarget = false;
                        choosen7.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human8")
                    {
                        targ = target8;
                        camScript.attackTarget = camScript.attackTarget8;
                        RP = rotatePoint8;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        choosen8.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human9")
                    {
                        targ = target9;
                        camScript.attackTarget = camScript.attackTarget9;
                        RP = rotatePoint9;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        mouseOnTarget = false;
                        choosen9.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human10")
                    {
                        targ = target10;
                        camScript.attackTarget = camScript.attackTarget10;
                        RP = rotatePoint10;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        mouseOnTarget = false;
                        choosen10.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human11")
                    {
                        targ = target11;
                        camScript.attackTarget = camScript.attackTarget11;
                        RP = rotatePoint11;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        mouseOnTarget = false;
                        choosen11.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human12")
                    {
                        targ = target12;
                        camScript.attackTarget = camScript.attackTarget12;
                        RP = rotatePoint12;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        mouseOnTarget = false;
                        choosen12.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Human13")
                    {
                        targ = target13;
                        camScript.attackTarget = camScript.attackTarget13;
                        RP = rotatePoint13;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        choosen13.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "Hunter")
                    {
                        targ = target14;
                        camScript.attackTarget = camScript.attackTarget14;
                        RP = rotatePoint14;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                    }
                    else if (hit.collider.gameObject.name == "Human15")
                    {
                        targ = target15;
                        camScript.attackTarget = camScript.attackTarget15;
                        RP = rotatePoint15;
                        if (skull != null)
                        {
                            skull = null;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint = null;
                        }
                        choosen15.gameObject.SetActive(false);
                    }
                    else if (hit.collider.gameObject == skull)
                    {
                        targ = skull.transform;
                    }
                    else if (hit.collider.gameObject == skullNoPoint)
                    {
                        targ = skullNoPoint.transform;
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
                if (theChoosen1 == 1)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human1.position.x, human1.position.y + 1f, human1.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human1.position.x, human1.position.y + 1f, human1.position.z));
                    randomkill += 1;
                }
                FindObjectOfType<AudioManager>().Play("Pop");
                human1.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target2)
            {
                if (theChoosen1 == 2)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human2.position.x, human2.position.y + 1f, human2.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human2.position.x, human2.position.y + 1f, human2.position.z));
                    randomkill += 1;
                }
                human2.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target3)
            {
                if (theChoosen1 == 3)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human3.position.x, human3.position.y + 1f, human3.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human3.position.x, human3.position.y + 1f, human3.position.z));
                    randomkill += 1;
                }
                human3.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target4)
            {
                if (theChoosen1 ==4)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human4.position.x, human4.position.y + 1f, human4.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human4.position.x, human4.position.y + 1f, human4.position.z));
                    randomkill += 1;
                }
                human4.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target5)
            {
                if (theChoosen2 == 5)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human5.position.x, human5.position.y + 1f, human5.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human5.position.x, human5.position.y + 1f, human5.position.z));
                    randomkill += 1;
                }
                human5.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target6)
            {
                if (theChoosen2 == 6)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human6.position.x, human6.position.y + 1f, human6.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human6.position.x, human6.position.y + 1f, human6.position.z));
                    randomkill += 1;
                }
                human6.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target7)
            {
                if (theChoosen2 == 7)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human7.position.x, human7.position.y + 1f, human7.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human7.position.x, human7.position.y + 1f, human7.position.z));
                    randomkill += 1;
                }
                human7.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target8)
            {
                if (theChoosen2 == 8)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human8.position.x, human8.position.y + 1f, human8.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human8.position.x, human8.position.y + 1f, human8.position.z));
                    randomkill += 1;
                }
                human8.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target9)
            {
                if (theChoosen2 == 9)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human9.position.x, human9.position.y + 1f, human9.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human9.position.x, human9.position.y + 1f, human9.position.z));
                    randomkill += 1;
                }
                human9.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target10)
            {
                if (theChoosen3 == 10)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human10.position.x, human10.position.y + 1f, human10.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human10.position.x, human10.position.y + 1f, human10.position.z));
                    randomkill += 1;
                }
                human10.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target11)
            {
                if (theChoosen3 == 11)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human11.position.x, human11.position.y + 1f, human11.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human11.position.x, human11.position.y + 1f, human11.position.z));
                    randomkill += 1;
                }
                human11.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target12)
            {
                Debug.Log("hej");
                if (theChoosen3 == 12)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human12.position.x, human12.position.y + 1f, human12.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human12.position.x, human12.position.y + 1f, human12.position.z));
                    randomkill += 1;
                }
                human12.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target13)
            {
                if (theChoosen3 == 13)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human13.position.x, human13.position.y + 1f, human13.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human13.position.x, human13.position.y + 1f, human13.position.z));
                    randomkill += 1;
                }
                human13.gameObject.SetActive(false);
                targ = null;
            }
            else if (targ == target14)
            {
                hunterDead = true;
                hunterSkull = SpawnObject("Prefabs/skull", new Vector3(human14.position.x, human14.position.y + 1f, human14.position.z));
                hunter.gameObject.SetActive(false);
                reachedHunter = false;
                targ = null;
            }
            else if (targ == target15)
            {
                if (theChoosen3 == 15)
                {
                    skull = SpawnObject("Prefabs/skull", new Vector3(human15.position.x, human15.position.y + 1f, human15.position.z));
                }
                else
                {
                    skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human15.position.x, human15.position.y + 1f, human15.position.z));
                    randomkill += 1;
                }
                human15.gameObject.SetActive(false);
                achivementList.ListKillGirl();
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
            Vector3 dir = RP.position - transform.position;
            dir.y = 0f;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, lookAtTargetSpeed * Time.deltaTime);
        }

        if ((reachedTarget || reachedHunter) && Input.GetMouseButtonDown(0))
        {
            if (pecks < peckAmountToKill)
            {
                pecks += 1;
                FindObjectOfType<AudioManager>().Play("Peck");
            }
              
        }
        #endregion

        #region AchivementList

        if (randomkill == randomkillAmount)
        {
            achivementList.ListKillMany();
        }
        if (randomkill == randomkillOne)
        {
            achivementList.ListKillOne();
        }
        if (inUnder)
        {
            achivementList.ListFlyUnder();
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
                    achivementList.ListPoop();
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
            birdMesh.enabled = true;
            invinsableTime = 5f;
        }
        if (invinsable)
        {
            if (rendererOnOff > 0)
            {
                rendererOnOff -= Time.deltaTime;
            }
            if (rendererOnOff <= 0)
            {
                birdMesh.enabled = !birdMesh.enabled;
                rendererOnOff = 0.3f;
            }
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
                achivementList.ListLoseLife();
                break;
            case 0:
                feather1.gameObject.SetActive(false);
                Lose();
                break;
        }

        #region skullDrop
        if (setBoolToFalse <= 0)
        {
            droppedSkull = false;
            skullRB.constraints = RigidbodyConstraints.FreezeAll;
            skull = null;
            skullNoPoint = null;
            setBoolToFalse = 8f;
        }
        if (droppedSkull)
        {
            setBoolToFalse -= Time.deltaTime;
            if (dropCount == 1)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos1.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos1.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 2)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos2.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos2.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 3)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos3.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos3.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 4)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos4.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos4.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 5)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos5.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos5.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 6)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos6.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos6.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 7)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos7.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos7.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 8)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos8.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos8.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 9)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos9.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos9.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 10)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos10.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos10.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 11)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos11.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos11.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 12)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos12.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos12.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 13)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos13.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos13.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 14)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos14.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos14.position, 1f * Time.deltaTime);
                }
            }
            if (dropCount == 15)
            {
                if (skull != null)
                {
                    skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos15.position, 1f * Time.deltaTime);
                }
                else if (skullNoPoint != null)
                {
                    skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, dropPos15.position, 1f * Time.deltaTime);
                }
            }
        }

        #endregion

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
        if ((reachedTarget || reachedHunter) && Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isPecking", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("isPecking", false);
        }
        if (anim.GetBool("isPecking") == true)
        {
            anim.Play("Peck");
        }
        if (anim.GetBool("isStopping") == true)
        {
            anim.Play("Stopping");
        }
        if (targetIsSet)
        {
            anim.SetBool("isStopping", true);
        }
        if (anim.GetBool("isDiving") == true)
        {
            anim.Play("Dive");
        }
        if (reachedHunter || reachedSkull || reachedTarget)
        {
            anim.SetBool("isDiving", false);
        }

        #endregion


        #region Caw

        if (cawTimer <= 0)
        {
            cawed = false;
            cawTimer = 2f;
        }
        if (cawed)
        {
            cawTimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Q) && !cawed)
        {
            FindObjectOfType<AudioManager>().Play("Caw");
            cawed = true;
            if (HumanZone)
            {
                caw += 1;
            }
            if (caw == cawOne)
            {
                achivementList.ListScare();
            }
            if (caw == cawAmount)
            {
                achivementList.ListScareTwo();
            }
        }
        #endregion

    }

    private void FixedUpdate()
    {
      
        if (!targetIsSet)
        {
            if (Luft)
            {
                //speed -= Time.deltaTime;
                //if( speed <= 1f)
                //{
                //    RB.velocity = new Vector3(0,0,0);
                //}
            }
            else
            {
                //speed = 3f;
            }
            #region movement
            if (!tutorialMode)
            {
                Vector3 locVel = transform.InverseTransformDirection(RB.velocity);
                locVel.z = speed;
                locVel.x = 0;
                locVel.y = Mathf.Clamp(locVel.y, maxFallSpeed, maxAscendSpeed);
                RB.velocity = transform.TransformDirection(locVel);

           
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
                else if (Input.GetKey(KeyCode.S) && !reachedTarget && !reachedHunter && !tutorialMode)
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
                    FindObjectOfType<AudioManager>().Play("Wosh");
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
                    if (Input.GetKey(KeyCode.D))
                    {
                        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
                        tiltZ = Mathf.Max(tiltZ - tiltSpeed * Time.deltaTime, -maxTilt);
                        RB.AddTorque(transform.up * turn, ForceMode.VelocityChange);
                        if (RB.angularVelocity.y >= maxVelocity)
                        {
                            RB.angularVelocity = new Vector3(RB.angularVelocity.x, maxVelocity, RB.angularVelocity.z);
                        }
                    }
                }
                if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                {
                    RB.angularVelocity = new Vector3(0, 0, 0);
                    if (tiltZ != 0)
                    {
                        tiltZ = tiltZ < 0 ? Mathf.Min(tiltZ + tiltSpeed * 2 * Time.deltaTime, 0) : Mathf.Max(tiltZ - tiltSpeed * 2 * Time.deltaTime, 0);
                    }
                }
                //if (turningLeft)
                //{
                //    float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
                //    tiltZ = Mathf.Min(tiltZ + tiltSpeed * Time.deltaTime, maxTilt);
                //    RB.AddTorque(transform.up * turn, ForceMode.VelocityChange);
                //    if (RB.angularVelocity.y <= -maxVelocity)
                //    {
                //        RB.angularVelocity = new Vector3(RB.angularVelocity.x, -maxVelocity, RB.angularVelocity.z);
                //    }
                //}
                //if (turningRight)
                //{
                //    float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
                //    tiltZ = Mathf.Max(tiltZ - tiltSpeed * Time.deltaTime, -maxTilt);
                //    RB.AddTorque(transform.up * turn, ForceMode.VelocityChange);
                //    if (RB.angularVelocity.y >= maxVelocity)
                //    {
                //        RB.angularVelocity = new Vector3(RB.angularVelocity.x, maxVelocity, RB.angularVelocity.z);
                //    }
                //}

                angles = new Vector3(tiltX, transform.eulerAngles.y, tiltZ);
                transform.rotation = Quaternion.Euler(angles);


                isAscending = false;

            }
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
                    if (inDropZone && hunterSkull != null)
                    {
                        hunterSkullDropped = true;
                        hunterSkull.transform.parent = null;
                    }
                    else if (inDropZone && skull != null)
                    {
                        FindObjectOfType<AudioManager>().Play("Point");
                        points += 1;
                        dropCount += 1;
                        droppedSkull = true;
                        skull.transform.parent = null;
                    }
                    else if (inDropZone && skullNoPoint != null)
                    {
                        dropCount += 1;
                        droppedSkull = true;
                        skullNoPoint.transform.parent = null;

                    }
                    else if (!inDropZone)
                    {
                        if (skull != null)
                        {
                            skull.transform.parent = null;
                            skullRB.useGravity = true;
                        }
                        else if (skullNoPoint != null)
                        {
                            skullNoPoint.transform.parent = null;
                            skullRB.useGravity = true;
                        }

                    }
                    targ = null;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    RB.constraints = RigidbodyConstraints.None;
                }
            }

            if (points == 1)
            {
                skull1.gameObject.SetActive(true);
                
            }
            if (points == 2)
            {
                skull2.gameObject.SetActive(true);
                
            }
            if (points >= 3)
            {
                skull3.gameObject.SetActive(true);
                doorAnim.SetBool("open", true);
                if (!hunterDead)
                {
                    if (!hunter.gameObject.activeInHierarchy)
                    {
                        hunter.gameObject.SetActive(true);
                        camScript.showHunter = true;
                    }

                }
            }
            //switch (points)
            //{
            //    case 1:
            //        skull1.gameObject.SetActive(true);
            //        if (droppedSkull)
            //        {
            //            skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos1.position, 1f * Time.deltaTime);
            //        }

            //        break;
            //    case 2:
            //        skull2.gameObject.SetActive(true);
            //        if (droppedSkull)
            //        {
            //            skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos2.position, 1f * Time.deltaTime);
            //        }
            //        break;
            //    case 3:
            //        skull3.gameObject.SetActive(true);
            //        if (droppedSkull)
            //        {
            //            skull.transform.position = Vector3.MoveTowards(skull.transform.position, dropPos3.position, 1f * Time.deltaTime);
            //        }
            //        if (!hunterDead)
            //        {
            //            if (!hunter.gameObject.activeInHierarchy)
            //            {
            //                hunter.gameObject.SetActive(true);
            //                camScript.showHunter = true;
            //            }
            //        }
            //        break;
            //}

            if (inWindZone)
            {
                RB.AddForce(WindZone.GetComponent<WindArea>().direction * WindZone.GetComponent <WindArea>().strength);
            }

            #endregion

        }
        else
        {
            RB.constraints = RigidbodyConstraints.FreezePosition;
            target = targ.position; 
            if (hunterSkull != null)
            {
                if (targ == hunterSkull.transform)
                {
                    target.y = targ.position.y + 0.26f;
                    target.x = targ.position.x;
                    target.z = targ.position.z;
                }
            }
            if (skullNoPoint != null)
            {
                if (targ == skullNoPoint.transform)
                {
                    target.y = targ.position.y + 0.26f;
                    target.x = targ.position.x;
                    target.z = targ.position.z;
                }
            }
            else if (skull != null)
            {
                if (targ == skull.transform)
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
                anim.SetBool("isStopping", false);
                anim.SetBool("isDiving", true);
                Attack();
            }
        }
    }
    public void Choose()
    {
        theChoosen1 = Random.Range(1, 5);
        theChoosen2 = Random.Range(5, 10);
        theChoosen3 = Random.Range(10, 14);

        if (theChoosen1 == 1)
        {
            choosen1.gameObject.SetActive(true);
            picture1.gameObject.SetActive(true);
        }
        if (theChoosen1 == 2)
        {
            choosen2.gameObject.SetActive(true);
            picture2.gameObject.SetActive(true);
        }
        if (theChoosen1 == 3)
        {
            choosen3.gameObject.SetActive(true);
            picture3.gameObject.SetActive(true);
        }
        if (theChoosen1 == 4)
        {
            choosen4.gameObject.SetActive(true);
            picture4.gameObject.SetActive(true);
        }
        if (theChoosen2 == 5)
        {
            choosen5.gameObject.SetActive(true);
            picture5.gameObject.SetActive(true);
        }
        if (theChoosen2 == 6)
        {
            choosen6.gameObject.SetActive(true);
            picture6.gameObject.SetActive(true);
        }
        if (theChoosen2 == 7)
        {
            choosen7.gameObject.SetActive(true);
            picture7.gameObject.SetActive(true);
        }
        if (theChoosen2 == 8)
        {
            choosen8.gameObject.SetActive(true);
            picture8.gameObject.SetActive(true);
        }
        if (theChoosen2 == 9)
        {
            choosen9.gameObject.SetActive(true);
            picture9.gameObject.SetActive(true);
        }
        if (theChoosen3 == 10)
        {
            choosen10.gameObject.SetActive(true);
            picture10.gameObject.SetActive(true);
        }
        if (theChoosen3 == 11)
        {
            choosen11.gameObject.SetActive(true);
            picture11.gameObject.SetActive(true);
        }
        if (theChoosen3 == 12)
        {
            choosen12.gameObject.SetActive(true);
            picture12.gameObject.SetActive(true);
        }
        if (theChoosen3 == 13)
        {
            choosen13.gameObject.SetActive(true);
            picture13.gameObject.SetActive(true);
        }
        if (theChoosen3 == 14)
        {
            //choosen14.gameObject.SetActive(true);
            //pictureHunter.gameObject.SetActive(true);
            //human14 = target14;
        }
        if (theChoosen3 == 15)
        {
            choosen15.gameObject.SetActive(true);
            picture15.gameObject.SetActive(true);
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
        if (hunterSkull != null)
        {
            if (targ == hunterSkull.transform)
            {
                hunterSkull.transform.parent = transform;
                hunterSkull.transform.localPosition = skullPickup;
                skullRB = hunterSkull.GetComponent<Rigidbody>();
                skullRB.useGravity = false;
            }
        }
        else if (skull != null)
        {
            if (targ == skull.transform)
            {
                skull.transform.parent = transform;
                skull.transform.localPosition = skullPickup;
                skullRB = skull.GetComponent<Rigidbody>();
                skullRB.useGravity = false;
            }
        }
        else if (skullNoPoint != null)
        {
            if (targ == skullNoPoint.transform)
            {
                skullNoPoint.transform.parent = transform;
                skullNoPoint.transform.localPosition = skullPickup;
                skullRB = skullNoPoint.GetComponent<Rigidbody>();
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
        if (col.gameObject.name == "skullNoPoint(Clone)")
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
        if (col.gameObject.tag == "Luft")
        {
            Luft = true;
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
        if (col.gameObject.name == "skullNoPoint(Clone)")
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
        if (col.gameObject.tag == "Luft")
        {
            Luft = false;
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