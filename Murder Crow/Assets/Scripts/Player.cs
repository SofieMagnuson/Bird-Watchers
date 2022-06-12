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
    public ChangeSettings settings;
    public SkinnedMeshRenderer birdMesh;
    public SkinnedMeshRenderer[] humanMeshes;
    public CapsuleCollider[] humanCColliders;
    public AudioManager AM;
    public PausMenu pause;
    public Credit winOrLoose;
    public int pecks, peckAmountToKill, points, pointsToWin, poops, poopAmount, caw, cawAmount, randomkill, randomkillAmount, theChosen1, theChosen2, theChosen3, dropCount;
    public float attackSpeed, waitUntilAttack, lookAtTargetSpeed, waitUntilMoving;
    public float setBoolToFalse, cawTimer, poopTimer;
    public bool targetIsSet, reachedTarget, reachedSkull, reachedSkullNoPoint, inDropZone, collided, inUnder, HumanZone, reachedHunter, hunterDead, hunterSkullDropped, tutorialMode;
    public bool inWindZone, droppedSkull, showedHunter, cawed, startedLose, startedWin, showingHuman, showedHuman;
    public LayerMask targetLayer, poopLayer;
    public Vector3 target, skullPickup, disToTH; 
    public Vector3? windDirection;
    public Transform targ, RP, human, winPos;
    public Transform[] dropPositions, humans, targets, rotatePoints;
    public Camera cam;
    public CameraMovement camScript;
    public Animator anim, doorAnim;
    public AnimationClip flapClip;
    public GameObject skull, skullNoPoint, hunterSkull, windZone, skullhunter, poop, chosenSkull, tutorialText, loseText, loseScreen, pileOfSkulls, winText, loseBack, allTT, loseWinButton, winBlack;
    public GameObject[] skulls, chosens, pictures, /*feathers,*/ poofs, hairs, texts, accessories;
    public Texture2D cursor;

    void Start()
    {
        //tutorialMode = true;
        points = 0;
        pointsToWin = 3;
        randomkill = 0;
        randomkillAmount = 3;
        attackSpeed = 13f;
        waitUntilAttack = 2f;
        waitUntilMoving = 2f;
        lookAtTargetSpeed = 2f;
        setBoolToFalse = 8f;
        cawTimer = 2f;
        poopTimer = 1f;
        pecks = 0;
        peckAmountToKill = 10;
        poops = 0;
        poopAmount = 40;
        caw = 0;
        dropCount = 0;
        cawAmount = 2;
        
        skullPickup = new Vector3(0, -0.186f, 0);
        RB = GetComponent<Rigidbody>();
        foreach (GameObject skull in skulls)
        {
            skull.SetActive(false);
        }
        //skulls[0].SetActive(false);
        //skulls[1].SetActive(false);
        //skulls[2].SetActive(false);
        //skulls[3].SetActive(false);
        //skulls[4].SetActive(false);
        skullhunter.SetActive(false);
        FindObjectOfType<AudioManager>().Play("InGame");

        Choose();
    }

    void Update()
    {
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
        else if (!tutorialMode && !camScript.introMode)
        {
            if (!showingHuman && !showedHuman)
            {
                StartCoroutine(ShowTutorialHuman());
                showingHuman = true;
            }
        }

        if (hunterSkullDropped && !skullhunter.activeInHierarchy)
        {
            skullhunter.gameObject.SetActive(true);
            StartCoroutine(Victory());
            AM.ChangeBGM(AM.clip);
            //FindObjectOfType<AudioManager>().Play("Twerk");
            //FindObjectOfType<AudioManager>().Stop("InGame");
            //Win();
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

        #region tutorialHuman
        if (humans[15].gameObject.activeInHierarchy)
        {
            disToTH = transform.position - humans[15].position;
            if (disToTH.magnitude > 30)
            {
                humans[15].gameObject.SetActive(false);
                Destroy(allTT);
            }
            if (targ != null && targ != targets[15])
            {
                humans[15].gameObject.SetActive(false);
                Destroy(allTT);
            }
        }
        else
        {
            if (skullNoPoint != null && allTT != null)
            {
                disToTH = transform.position - skullNoPoint.transform.position;
                if (disToTH.magnitude > 30)
                {
                    Destroy(skullNoPoint.gameObject);
                    skullNoPoint = null;
                    Destroy(allTT);
                }
            }
        }

        if (inDropZone && allTT != null && !tutorialMode && !showingHuman && reachedSkull)
        {
            if (texts[4] != null)
            {
                Destroy(texts[4].gameObject);
                texts[4] = null;
            }
            if (texts[5] != null && !texts[5].activeInHierarchy)
            {
                texts[5].SetActive(true);
            }
        }
        #endregion

        #region set target
        if (!targetIsSet && !reachedTarget && !reachedHunter && !reachedSkull && !collided && waitUntilMoving == 2 && !camScript.showHuman)
        {
            Vector3 mousePos = -Vector3.one;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 8f, targetLayer))
                {
                    mousePos = hit.point;
                    if (hit.collider.gameObject.name == "Human")
                    {
                        SetTarget(targets[0], camScript.attackTargets[0], rotatePoints[0], chosens[0]);
                    }
                    else if (hit.collider.gameObject.name == "Human2")
                    {
                        SetTarget(targets[1], camScript.attackTargets[1], rotatePoints[1], chosens[1]);
                    }
                    else if (hit.collider.gameObject.name == "Human3")
                    {
                        SetTarget(targets[2], camScript.attackTargets[2], rotatePoints[2], chosens[2]);
                    }
                    else if (hit.collider.gameObject.name == "Human4")
                    {
                        SetTarget(targets[3], camScript.attackTargets[3], rotatePoints[3], chosens[3]);
                    }
                    else if (hit.collider.gameObject.name == "Human5")
                    {
                        SetTarget(targets[4], camScript.attackTargets[4], rotatePoints[4], chosens[4]);
                    }
                    else if (hit.collider.gameObject.name == "Human6")
                    {
                        SetTarget(targets[5], camScript.attackTargets[5], rotatePoints[5], chosens[5]);
                    }
                    else if (hit.collider.gameObject.name == "Human7")
                    {
                        SetTarget(targets[6], camScript.attackTargets[6], rotatePoints[6], chosens[6]);
                    }
                    else if (hit.collider.gameObject.name == "Human8")
                    {
                        SetTarget(targets[7], camScript.attackTargets[7], rotatePoints[7], chosens[7]);
                    }
                    else if (hit.collider.gameObject.name == "Human9")
                    {
                        SetTarget(targets[8], camScript.attackTargets[8], rotatePoints[8], chosens[8]);
                    }
                    else if (hit.collider.gameObject.name == "Human10")
                    {
                        SetTarget(targets[9], camScript.attackTargets[9], rotatePoints[9], chosens[9]);
                    }
                    else if (hit.collider.gameObject.name == "Human11")
                    {
                        SetTarget(targets[10], camScript.attackTargets[10], rotatePoints[10], chosens[10]);
                    }
                    else if (hit.collider.gameObject.name == "Human12")
                    {
                        SetTarget(targets[11], camScript.attackTargets[11], rotatePoints[11], chosens[11]);
                    }
                    else if (hit.collider.gameObject.name == "Human13")
                    {
                        SetTarget(targets[12], camScript.attackTargets[12], rotatePoints[12], chosens[12]);
                    }
                    else if (hit.collider.gameObject.name == "Hunter")
                    {
                        targ = targets[13];
                        camScript.attackTarget = camScript.attackTargets[13];
                        RP = rotatePoints[13];
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
                        SetTarget(targets[14], camScript.attackTargets[14], rotatePoints[14], chosens[13]);
                    }
                    else if (hit.collider.gameObject.name == "TutorialHuman")
                    {
                        targ = targets[15];
                        camScript.attackTarget = camScript.attackTargets[15];
                        RP = rotatePoints[15];
                        texts[0].SetActive(false);
                    }
                    else if (hit.collider.gameObject.name == "skull(Clone)")
                    {
                        skull = hit.collider.gameObject;
                        targ = skull.transform;
                    }
                    else if (hit.collider.gameObject.name == "skullNoPoint(Clone)")
                    {
                        skullNoPoint = hit.collider.gameObject;
                        targ = skullNoPoint.transform;
                        if (allTT != null)
                        {
                            texts[2].SetActive(false);
                        }
                    }
                    else if (hit.collider.gameObject == hunterSkull)
                    {
                        targ = hunterSkull.transform;
                    }
                    targetIsSet = true;
                    FindObjectOfType<AudioManager>().Play("Flapping");
                }
            }
        }
        #endregion

        if (targetIsSet)
        {
            target = targ.position;
            if (hunterSkull != null)
            {
                if (targ == hunterSkull.transform)
                {
                    PlaceSkullInPosition();
                }
            }
            if (skullNoPoint != null)
            {
                if (targ == skullNoPoint.transform)
                {
                    PlaceSkullInPosition();
                }
            }
            else if (skull != null)
            {
                if (targ == skull.transform)
                {
                    PlaceSkullInPosition();
                }
            }
            Vector3 dir = target - transform.position;
            dir.y = 0f;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, lookAtTargetSpeed * Time.fixedDeltaTime);
            waitUntilAttack -= Time.deltaTime;
            if (waitUntilAttack <= 0)
            {
                anim.SetBool("isStopping", false);
                anim.SetBool("isDiving", true);
                Attack();
            }
        }

        #region attacking

        if (pecks == peckAmountToKill)
        {
            if (targ == targets[0])
            {
                KillHuman(theChosen1, 1, humans[0], poofs[0], humanMeshes[0], humanCColliders[0], hairs[0]);
            }
            else if (targ == targets[1])
            {
                KillHuman(theChosen1, 2, humans[1], poofs[1], humanMeshes[1], humanCColliders[1], hairs[1]);
            }
            else if (targ == targets[2])
            {
                KillHuman(theChosen1, 3, humans[2], poofs[2], humanMeshes[2], humanCColliders[2], hairs[2]);
            }
            else if (targ == targets[3])
            {
                KillHuman(theChosen1, 4, humans[3], poofs[3], humanMeshes[3], humanCColliders[3], hairs[3]);
            }
            else if (targ == targets[4])
            {
                KillHuman(theChosen2, 5, humans[4], poofs[4], humanMeshes[4], humanCColliders[4], hairs[4]);
            }
            else if (targ == targets[5])
            {
                KillHuman(theChosen2, 6, humans[5], poofs[5], humanMeshes[5], humanCColliders[5], hairs[5]);
            }
            else if (targ == targets[6])
            {
                KillHuman(theChosen2, 7, humans[6], poofs[6], humanMeshes[6], humanCColliders[6], hairs[6]);
            }
            else if (targ == targets[7])
            {
                KillHuman(theChosen2, 8, humans[7], poofs[7], humanMeshes[7], humanCColliders[7], hairs[7]);
                accessories[0].SetActive(false);
            }
            else if (targ == targets[8])
            {
                KillHuman(theChosen2, 9, humans[8], poofs[8], humanMeshes[8], humanCColliders[8], hairs[8]);
            }
            else if (targ == targets[9])
            {
                KillHuman(theChosen3, 10, humans[9], poofs[9], humanMeshes[9], humanCColliders[9], hairs[9]);
            }
            else if (targ == targets[10])
            {
                KillHuman(theChosen3, 11, humans[10], poofs[10], humanMeshes[10], humanCColliders[10], hairs[10]);
            }
            else if (targ == targets[11])
            {
                KillHuman(theChosen3, 12, humans[11], poofs[11], humanMeshes[11], humanCColliders[11], hairs[11]);
            }
            else if (targ == targets[12])
            {
                KillHuman(theChosen3, 13, humans[12], poofs[12], humanMeshes[12], humanCColliders[12], hairs[12]);
            }
            else if (targ == targets[13])
            {
                hunterDead = true;
                hunterSkull = SpawnObject("Prefabs/skull", new Vector3(humans[13].position.x, humans[13].position.y + 0.5f, humans[13].position.z));
                reachedHunter = false;
                human = humans[13];
                if (!poofs[13].activeInHierarchy)
                {
                    poofs[13].gameObject.SetActive(true);
                }
                if (humanMeshes[13].enabled)
                {
                    accessories[1].SetActive(false);
                    accessories[2].SetActive(false);
                    accessories[3].SetActive(false);
                    accessories[4].SetActive(false);
                    hairs[13].SetActive(false);
                    humanCColliders[13].enabled = false;
                    humanMeshes[13].gameObject.SetActive(false);
                }
                StartCoroutine(PlayPoof());
                targ = null;
            }
            else if (targ == targets[14])
            {
                KillHuman(theChosen3, 14, humans[14], poofs[14], humanMeshes[14], humanCColliders[14], hairs[14]);
                if (!achivementList.killedGirl)
                {
                    achivementList.ListKillGirl();
                }
            }
            else if (targ == targets[15])
            {
                texts[1].SetActive(false);
                texts[2].SetActive(true);
                skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(humans[15].position.x, humans[15].position.y + 0.5f, humans[15].position.z));
                FindObjectOfType<AudioManager>().Play("Pop");
                human = humans[15];
                if (!poofs[15].activeInHierarchy)
                {
                    poofs[15].SetActive(true);
                }
                if (humanMeshes[15].enabled == true)
                {
                    hairs[15].SetActive(false);
                    humanCColliders[15].enabled = false;
                    humanMeshes[15].gameObject.SetActive(false);
                }
                StartCoroutine(PlayPoof());
                HumanZone = false;
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
            FindObjectOfType<AudioManager>().Play("Pop");

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

        if (randomkill == randomkillAmount && !achivementList.killedMany)
        {
            achivementList.ListKillMany();
        }
        if (randomkill == 1 && !achivementList.killedOne)
        {
            achivementList.ListKillOne();
        }
        if (inUnder && !achivementList.flewUnder)
        {
            achivementList.ListFlyUnder();
        }


        #endregion

        #region pooping
        if (poopTimer > 0)
        {
            poopTimer -= Time.deltaTime;
        }
        else
        {
            poopTimer = 0;
        }
        if (Input.GetKeyDown(KeyCode.E) && poopTimer <= 0 && !tutorialMode)
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
                if (poops == poopAmount && !achivementList.pooped)
                {
                    achivementList.ListPoop();
                }


            }
            poopTimer = 1f;
  
        }
        #endregion

        #region skullPickup

        if (reachedSkull)
        {
            if (Input.GetMouseButton(0))
            {
                PickUpSkull();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if(inDropZone)
                {
                    if (allTT != null)
                    {
                        if (texts[5] != null)
                        {
                            Destroy(texts[5].gameObject);
                        }
                        texts[6].SetActive(true);
                        StartCoroutine(DestroyTutorialText());
                    }
                    if (hunterSkull != null)
                    {
                        hunterSkullDropped = true;
                        hunterSkull.transform.parent = null;
                    }
                    else if (skull != null)
                    {
                        FindObjectOfType<AudioManager>().Play("Point");
                        IncreasePoints();
                        dropCount += 1;
                        droppedSkull = true;
                        skull.transform.parent = null;
                    }
                    else if (skullNoPoint != null)
                    {
                        dropCount += 1;
                        droppedSkull = true;
                        skullNoPoint.transform.parent = null;
                    }
                }
                else
                {
                    SetSkullToNull();
                }
                targ = null;
            }
            if (Input.GetKey(KeyCode.W))
            {
                RB.constraints = RigidbodyConstraints.None;
            }
        }
        #endregion

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
            DropSkullInNest(dropPositions[dropCount - 1]);
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
        if (anim.GetBool("isPecking"))
        {
            anim.Play("Peck");
        }
        if (anim.GetBool("isStopping") == true)
        {
            anim.Play("Stopping");
        }
        if (targetIsSet)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Dive"))
            anim.SetBool("isStopping", true);
        }
        if (anim.GetBool("isDiving") == true && !anim.GetCurrentAnimatorStateInfo(0).IsName("Dive"))
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
            if (HumanZone)
            {
                FindObjectOfType<AudioManager>().Play("Gasp");
                caw += 1;
            }
            if (caw == 1 && !achivementList.scared)
            {
                achivementList.ListScare();
            }
            if (caw == cawAmount && !achivementList.scaredTwo)
            {
                achivementList.ListScareTwo();
            }
            cawed = true;
        }
        #endregion
    }

    public void Attack()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(target.x, target.y, target.z);
        if (Vector3.Distance(startPos, endPos) < 0.1f)
        {
            startPos = endPos;
        }
        else
        {
            transform.position = Vector3.MoveTowards(startPos, endPos, attackSpeed * Time.fixedDeltaTime);
        }
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
        FindObjectOfType<AudioManager>().Play("Pickup");
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
                if (texts[3] != null || texts[4] != null)
                {
                    Destroy(texts[3].gameObject);
                    if (texts[4] != null)
                    {
                        texts[4].SetActive(true);

                    }
                }
                skullNoPoint.transform.parent = transform;
                skullNoPoint.transform.localPosition = skullPickup;
                skullRB = skullNoPoint.GetComponent<Rigidbody>();
                skullRB.useGravity = false;
            }
        }
    }

    private void IncreasePoints()
    {
        skulls[points].SetActive(true);
        points += 1;

        if(points >= 3)  
        {
            doorAnim.SetBool("open", true);
            if (!hunterDead && !hunter.gameObject.activeInHierarchy)
            {
                hunter.gameObject.SetActive(true);
                camScript.showHunter = true;
            }
        }
    }

    private void SetTarget(Transform target, Transform camTarget, Transform rotatePoint, GameObject chosen)
    {
        targ = target;
        camScript.attackTarget = camTarget;
        RP = rotatePoint;
        if (skull != null)
        {
            skull = null;
        }
        else if (skullNoPoint != null)
        {
            skullNoPoint = null;
        }
        chosen.gameObject.SetActive(false);
    }

    private void KillHuman(int chosen, int number, Transform human, GameObject poof, SkinnedMeshRenderer mesh, CapsuleCollider col, GameObject hair)
    {
        if (chosen == number)
        {
            skull = SpawnObject("Prefabs/skull", new Vector3(human.position.x, human.position.y + 0.5f, human.position.z));
        }
        else
        {
            skullNoPoint = SpawnObject("Prefabs/skullNoPoint", new Vector3(human.position.x, human.position.y + 0.5f, human.position.z));
            randomkill += 1;
        }
        FindObjectOfType<AudioManager>().Play("Pop");
        this.human = human;
        if (!poof.activeInHierarchy)
        {
            poof.SetActive(true);
        }
        if (mesh.enabled == true)
        {
            hair.SetActive(false);
            mesh.gameObject.SetActive(false);
        }
        if (col.enabled)
        {
            col.enabled = false;
        }
        StartCoroutine(PlayPoof());
        HumanZone = false;
        targ = null;
    }

    private void DropSkullInNest(Transform target)
    {
        if (skull != null)
        {
            if (Vector3.Distance(skull.transform.position, target.position) < 1f * Time.deltaTime)
            {
                skull.transform.position = target.position;
            }
            else
            {
                skull.transform.position = Vector3.MoveTowards(skull.transform.position, target.position, 1f * Time.deltaTime);
            }
        }
        else if (skullNoPoint != null)
        {
            if (Vector3.Distance(skullNoPoint.transform.position, target.position) < 1f * Time.deltaTime)
            {
                skullNoPoint.transform.position = target.position;
            }
            else
            {
                skullNoPoint.transform.position = Vector3.MoveTowards(skullNoPoint.transform.position, target.position, 1f * Time.deltaTime);
            }
        }
    }

    private void SetSkullToNull()
    {
        if (skull != null)
        {
            skull.transform.parent = null;
            skullRB.useGravity = true;
            skull.transform.rotation = Quaternion.identity;
        }
        else if (skullNoPoint != null)
        {
            skullNoPoint.transform.parent = null;
            skullRB.useGravity = true;
            skullNoPoint.transform.rotation = Quaternion.identity;
        }
        else if (hunterSkull != null)
        {
            hunterSkull.transform.parent = null;
            skullRB.useGravity = true;
            hunterSkull.transform.rotation = Quaternion.identity;
        }
        targ = null;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "human" && targetIsSet)
        {
            reachedTarget = true;
        }
        if (col.gameObject.name == "TutorialHuman")
        {
            texts[1].SetActive(true);
        }
        if (col.gameObject.name == "Hunter" && targetIsSet)
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
            if (texts[3] != null)
            {
                texts[3].SetActive(true);
            }
        }
        if (col.gameObject.name == "DropSkullArea")
        {
            inDropZone = true;
        }
        if (col.gameObject.tag == "WindArea")
        {
            windZone = col.gameObject;
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
            windDirection = col.gameObject.transform.forward;
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
            windZone = col.gameObject;
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
            windDirection = null;
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
            if (!targetIsSet)
            {
                RB.constraints = RigidbodyConstraints.FreezeRotation;
                FindObjectOfType<AudioManager>().Play("Collision");
                StartCoroutine("Invincible");
            }
            else
            {
                targetIsSet = false;
                anim.SetBool("isDiving", false);
                RB.constraints = RigidbodyConstraints.FreezeRotation;
                FindObjectOfType<AudioManager>().Play("Collision");
                StartCoroutine("Invincible");
            }
            if (reachedSkull || reachedSkullNoPoint)
            {
                SetSkullToNull();
            }
        }
    }
    public void Choose()
    {
        theChosen1 = Random.Range(1, 5);
        theChosen2 = Random.Range(5, 10);
        theChosen3 = Random.Range(10, 14);

        switch (theChosen1)
        {
            case 1:
                chosens[0].gameObject.SetActive(true);
                pictures[0].gameObject.SetActive(true);
                break;
            case 2:
                chosens[1].gameObject.SetActive(true);
                pictures[1].gameObject.SetActive(true);
                break;
            case 3:
                chosens[2].gameObject.SetActive(true);
                pictures[2].gameObject.SetActive(true);
                break;
            case 4:
                chosens[3].gameObject.SetActive(true);
                pictures[3].gameObject.SetActive(true);
                break;
        }
        switch (theChosen2)
        {
            case 5:
                chosens[4].gameObject.SetActive(true);
                pictures[4].gameObject.SetActive(true);
                break;
            case 6:
                chosens[5].gameObject.SetActive(true);
                pictures[5].gameObject.SetActive(true);
                break;
            case 7:
                chosens[6].gameObject.SetActive(true);
                pictures[6].gameObject.SetActive(true);
                break;
            case 8:
                chosens[7].gameObject.SetActive(true);
                pictures[7].gameObject.SetActive(true);
                break;
            case 9:
                chosens[8].gameObject.SetActive(true);
                pictures[8].gameObject.SetActive(true);
                break;
        }
        switch (theChosen3)
        {
            case 10:
                chosens[9].gameObject.SetActive(true);
                pictures[9].gameObject.SetActive(true);
                break;
            case 11:
                chosens[10].gameObject.SetActive(true);
                pictures[10].gameObject.SetActive(true);
                break;
            case 12:
                chosens[11].gameObject.SetActive(true);
                pictures[11].gameObject.SetActive(true);
                break;
            case 13:
                chosens[12].gameObject.SetActive(true);
                pictures[12].gameObject.SetActive(true);
                break;
            case 14:
                chosens[13].gameObject.SetActive(true);
                pictures[13].gameObject.SetActive(true);
                break;
        }
    }

    private void PlaceSkullInPosition()
    {
        target.y = targ.position.y + 0.26f;
        target.x = targ.position.x;
        target.z = targ.position.z;
    }

    private void Lose()
    {
        StartCoroutine(CallLose());
    }

    private void Win()
    {
        SceneManager.LoadScene("Win");
    }

    private IEnumerator Victory()
    {
        RB.constraints = RigidbodyConstraints.FreezeAll;
        winBlack.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        startedWin = true;
        transform.position = winPos.position;
        pileOfSkulls.SetActive(true);
        anim.SetBool("won", true);
        yield return new WaitForSeconds(2.5f);
        loseWinButton.gameObject.SetActive(true);
        winText.SetActive(true);
    }

    private IEnumerator CallLose()
    {
        RB.constraints = RigidbodyConstraints.FreezeAll;
        anim.SetBool("lost", true);
        yield return new WaitForSeconds(1.5f);
        loseText.SetActive(true);
        loseBack.SetActive(true);
        loseWinButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        loseScreen.gameObject.SetActive(true);
    }

    private IEnumerator Invincible()
    {
        collided = true;
        yield return new WaitForSeconds(1.0f);
        birdCol.enabled = false;
        RB.constraints = RigidbodyConstraints.None;
        yield return new WaitForSeconds(5.0f);
        birdCol.enabled = true;
        collided = false;
    }

    private IEnumerator ShowTutorialHuman()
    {
        yield return new WaitForSeconds(1.5f);
        RB.constraints = RigidbodyConstraints.FreezeAll;
        camScript.showHuman = true;
        yield return new WaitForSeconds(2f);
        RB.constraints = RigidbodyConstraints.None;
        showingHuman = false;
        showedHuman = true;
        camScript.showHuman = false;
        texts[0].SetActive(true);
    }

    private IEnumerator PlayPoof()
    {
        yield return new WaitForSeconds(4f);
        human.gameObject.SetActive(false);
    }

    private IEnumerator DestroyTutorialText()
    {
        yield return new WaitForSeconds(3f);
        Destroy(allTT);
    }
}