using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public Player player;
    public PausMenu pause;
    public Credit winOrLoose;
    public Vector3 offset, flyingOffset, noMovingOffset, targetOffset, tutorialOffset, showingHunterPos, loseOffset, showingHumanPos;
    private float camSpeed;
    public float tilt, maxTilt, tiltSpeed, FOV, maxFOV, FOVspeed, showingTime, lookAtTargetSpeed, timeUntilRA, lookingRA, lookBack; 
    public float timeUntilPicnic, lookingPicnic, lookingNest, timeUntilTutorialMode, zRot;
    public Vector3 velocity, introDefaultRot, nestSpot;
    public bool attackMode, showHunter, introMode, showedRA, reachedSpot1, reachedSpot2, lookedBack, showedPicnic, showedNest, waited, showHuman;
    public Transform attackTarget, losePos, tutorialHuman;
    public Transform hunterLookAtPoint, introPoint, roundAboutPoint, picnicPoint, tutorialPoint, winPos;
    public Transform[] attackTargets;
    public Vector2 rotation = new Vector2(0, 0);
    public GameObject skip;

    void Start()
    {
        introMode = true;

        camSpeed = 0.35f;
        flyingOffset = new Vector3(0.0f, 1.2f, -0.5f);
        noMovingOffset = new Vector3(0.0f, 1f, -1f);
        targetOffset = new Vector3(0.0f, 1.5f, -1f);
        tutorialOffset = new Vector3(0.0f, 0.2f, 0.3f);
        loseOffset = new Vector3(0.0f, 0.3f, -1.3f);
        showingHunterPos = new Vector3(110.92f, 10.14f, -632.4f);
        showingHumanPos = new Vector3(11.65f, 10.91f, -645.06f);
        nestSpot = new Vector3(-1.083397f, 15.144f, -658.4306f);
        offset = tutorialOffset;
        velocity = Vector3.one;
        tilt = 0f;
        FOV = 45;
        FOVspeed = 3f;
        tiltSpeed = 10f;
        maxTilt = 20f;
        maxFOV = 55;
        cam.fieldOfView = 45;
        showingTime = 6f;
        lookAtTargetSpeed = 1f;
        timeUntilRA = 1f;
        lookingRA = 4f;
        lookBack = 2f;
        timeUntilPicnic = 2f;
        lookingPicnic = 3f;
        lookingNest = 4f;
        timeUntilTutorialMode = 2f;
    }

    void Update()
    {
        if (introMode)
        {
            HandleIntroMode();
        }
        else
        {
            skip.gameObject.SetActive(false);
            if (!player.tutorialMode)
            {
                if (player.reachedTarget || player.reachedHunter)
                {
                    SetAttackMode();
                }
                else if (!player.reachedTarget && !player.reachedHunter && !showHunter && !player.startedLose && !showHuman)
                {
                    if (cam.fieldOfView >= 64)
                    {
                        cam.fieldOfView = 64;
                    }
                    else if (cam.fieldOfView != 64)
                    {
                        cam.fieldOfView += 8f * Time.deltaTime;
                    }
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                camSpeed = 0.27f;
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                camSpeed = 0.35f;
            }
            if (player.tutorialMode && player.transform.rotation.eulerAngles.y > 4 && player.transform.rotation.eulerAngles.y < 76)
            {
                player.tutorialText.gameObject.SetActive(true);
                if (Input.GetKey(KeyCode.W))
                {
                    offset = flyingOffset;
                    player.tutorialText.gameObject.SetActive(false);
                    player.RB.useGravity = true;
                    player.RB.isKinematic = false;
                    player.birdMesh.enabled = true;
                    player.tutorialMode = false;
                }
            }
            else if (player.tutorialMode && (player.transform.rotation.eulerAngles.y < 4 || player.transform.rotation.eulerAngles.y > 76))
            {
                player.tutorialText.gameObject.SetActive(false);
            }

            if (player.targetIsSet)
            {
                offset = targetOffset;
            }
            if (player.reachedTarget && Input.GetKey(KeyCode.W))
            {
                offset = flyingOffset;
            }
            if (showingTime <= 0)
            {
                showHunter = false;
                showingTime = 0;
            }

            if (showHunter)
            {
                ShowHunter();
            }

            if (showHuman)
            {
                ShowHuman();
            }

            if (player.startedLose)
            {
                offset = loseOffset;
                transform.LookAt(player.transform);
                if (cam.fieldOfView <= 40)
                {
                    cam.fieldOfView = 40;
                }
                else if (cam.fieldOfView != 40)
                {
                    cam.fieldOfView -= FOVspeed * Time.deltaTime;
                }
            }

            if (player.startedWin)
            {
                SetWinMode();
            }
        }
    }

    void LateUpdate()
    {
        if (!player.reachedTarget && !player.reachedHunter && !player.tutorialMode && !showHunter && !introMode && !player.startedWin && !showHuman)
        {
            #region tilt
            //if (Input.GetKey(KeyCode.D))
            //{
            //    tilt = Mathf.Max(tilt - tiltSpeed * Time.deltaTime, -maxTilt);
            //}
            //else if (Input.GetKey(KeyCode.A))
            //{
            //    tilt = Mathf.Min(tilt + tiltSpeed * Time.deltaTime, maxTilt);
            //} else if (tilt != 0)
            //{
            //    tilt = tilt < 0 ? Mathf.Min(tilt + tiltSpeed * 2 * Time.deltaTime, 0) : Mathf.Max(tilt - tiltSpeed * 2 * Time.deltaTime, 0);
            //}
            #endregion

            Vector3 targetPos = target.position + (target.rotation * offset);
            Vector3 camPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, camSpeed);
            transform.position = camPos;

            Quaternion camRot = Quaternion.Euler(new Vector3(target.eulerAngles.x + 35f, target.eulerAngles.y, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, camRot, (camSpeed + 4) * Time.deltaTime);
        }
        if (player.tutorialMode)
        {
            Vector3 targetPos = target.position + (target.rotation * offset);
            transform.position = targetPos;

            Vector3 camRot = new Vector3(target.eulerAngles.x + 17f, target.eulerAngles.y, 0);
            transform.rotation = Quaternion.Euler(camRot);
        }
    }

    private void HandleIntroMode()
    {
        skip.gameObject.SetActive(true);
        introDefaultRot = new Vector3(transform.position.x - 10f, transform.position.y - 5f, transform.position.z - 7f);
        if (!reachedSpot1)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = introPoint.position;
            if (Vector3.Distance(startPos, endPos) < 10f * Time.deltaTime)
            {
                transform.position = endPos;
                reachedSpot1 = true;
            }
            else
            {
                transform.position = Vector3.MoveTowards(startPos, endPos, 10f * Time.deltaTime);
            }
        }
        else if (reachedSpot1 && !reachedSpot2)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = nestSpot;
            if (Vector3.Distance(startPos, endPos) < 10f * Time.deltaTime)
            {
                transform.position = endPos;
                showedNest = true;
                reachedSpot2 = true;
            }
            else
            {
                transform.position = Vector3.MoveTowards(startPos, endPos, 10f * Time.deltaTime);
            }
        }
        if (reachedSpot2 && timeUntilTutorialMode > 0)
        {
            Vector3 dir = tutorialPoint.position - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, lookAtTargetSpeed * Time.deltaTime);
            timeUntilTutorialMode -= Time.deltaTime;
        }
        if (timeUntilTutorialMode <= 0)
        {
            introMode = false;
            timeUntilTutorialMode = 0;
            player.tutorialMode = true;

        }
        timeUntilRA -= Time.deltaTime;
        if (timeUntilRA <= 0 && !showedRA)
        {
            timeUntilRA = 0;
            Vector3 dir = roundAboutPoint.position - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, lookAtTargetSpeed * Time.deltaTime);
            lookingRA -= Time.deltaTime;
        }
        if (lookingRA <= 0 && !lookedBack)
        {
            showedRA = true;
            lookingRA = 0;
            Vector3 dir = introDefaultRot - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, (lookAtTargetSpeed - 0.4f) * Time.deltaTime);
            lookBack -= Time.deltaTime;
        }
        if (lookBack <= 0 && !waited)
        {
            lookedBack = true;
            lookBack = 0;
            timeUntilPicnic -= Time.deltaTime;
        }
        if (timeUntilPicnic <= 0 && !showedPicnic)
        {
            timeUntilPicnic = 0;
            waited = true;
            Vector3 dir = picnicPoint.position - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, (lookAtTargetSpeed) * Time.deltaTime);
            lookingPicnic -= Time.deltaTime;
        }
        if (lookingPicnic <= 0 && !showedNest)
        {
            lookingPicnic = 0;
            showedPicnic = true;
            Vector3 dir = nestSpot - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, (lookAtTargetSpeed + 0.9f) * Time.deltaTime);
            lookingNest -= Time.deltaTime;
        }
    }



    void SetAttackMode()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = attackTarget.position;
        transform.position = Vector3.Lerp(startPos, endPos, 4 * Time.deltaTime);
        transform.LookAt(new Vector3(player.target.x, player.target.y - 0.2f, player.target.z));
        if (cam.fieldOfView <= 42)
        {
            cam.fieldOfView = 42;
        }
        else if (cam.fieldOfView != 42)
        {
            cam.fieldOfView -= FOVspeed * Time.deltaTime; 
        }
    }

    void SetWinMode()
    {
        transform.RotateAround(player.transform.position, player.transform.up, 10f * Time.deltaTime);
        Vector3 delta = transform.position - player.transform.position;
        delta.y = 0.25f;
        transform.position = player.transform.position + delta.normalized * 1;
    }

    public void ShowHuman()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = showingHumanPos;
        transform.position = Vector3.Lerp(startPos, endPos, 2 * Time.deltaTime);
        transform.LookAt(new Vector3(tutorialHuman.position.x, tutorialHuman.position.y + 1f, tutorialHuman.position.z));
    }

    public void ShowHunter()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = showingHunterPos;
        transform.position = Vector3.Lerp(startPos, endPos, 4 * Time.deltaTime);
        transform.LookAt(hunterLookAtPoint.position);
        if (cam.fieldOfView <= 37)
        {
            cam.fieldOfView = 37;
        }
        else if (cam.fieldOfView != 37)
        {
            cam.fieldOfView -= (FOVspeed + 1) * Time.deltaTime;
        }
        showingTime -= Time.deltaTime;
    }

    public void SkipIntro()
    {
        introMode = false;
        player.tutorialMode = true;
    }
}
