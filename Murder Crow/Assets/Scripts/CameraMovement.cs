using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public Player player;
    public Vector3 offset, flyingOffset, noMovingOffset, targetOffset, tutorialOffset, showingHunterPos;
    private float camSpeed, mouseSensitivity;
    public float tilt, maxTilt, tiltSpeed, FOV, maxFOV, FOVspeed, showingTime, showRoundabout, showPicnic, lookAtTargetSpeed, lookAtPlaces, timeAtPicnic;
    public Vector3 velocity, camRot, introDefaultRot;
    public bool attackMode, showHunter, introMode, showingRoundabout, showingPicnic, goBack, showedRA, reachedSpot;
    public Transform attackTarget1, attackTarget2, attackTarget3, attackTarget, attackTarget4, attackTarget5, attackTarget6, attackTarget7, attackTarget8, attackTarget9, attackTarget10, attackTarget11, attackTarget12, attackTarget13, attackTarget14, attackTarget15;
    public Transform hunterLookAtPoint, introPoint, roundAboutPoint, picnicPoint;
    public Vector2 rotation = new Vector2(0, 0);

    void Start()
    {
        camSpeed = 0.35f;
        flyingOffset = new Vector3(0.0f, 1.2f, -0.5f);
        noMovingOffset = new Vector3(0.0f, 1f, -1f);
        targetOffset = new Vector3(0.0f, 1.5f, -1f);
        tutorialOffset = new Vector3(0.0f, 0.2f, 0.3f);
        showingHunterPos = new Vector3(110.92f, 10.14f, -632.4f);
        offset = tutorialOffset;
        velocity = Vector3.one;
        tilt = 0f;
        FOV = 45;
        FOVspeed = 3f;
        tiltSpeed = 10f;
        maxTilt = 20f;
        maxFOV = 55;
        mouseSensitivity = 0.03f;
        cam.fieldOfView = 45;
        showingTime = 6f;
        showRoundabout = 1f;
        showPicnic = 9f;
        introMode = true;
        lookAtTargetSpeed = 1f;
        lookAtPlaces = 4f;
        timeAtPicnic = 3f;

        StartCoroutine(Intro());
    }

    void Update()
    {
        if (introMode)
        {
            introDefaultRot = new Vector3(transform.position.x - 10f, transform.position.y - 5f, transform.position.z - 7f);
            if (!reachedSpot)
            {
                Vector3 startPos = transform.position;
                Vector3 endPos = introPoint.position;
                if (Vector3.Distance(startPos, endPos) < 10f * Time.deltaTime)
                {
                    transform.position = endPos;
                    reachedSpot = true;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(startPos, endPos, 10f * Time.deltaTime);
                }
            }
            else
            {
                Vector3 startPos = transform.position;
                Vector3 endPos = player.transform.position;
                if(Vector3.Distance(startPos, endPos) < 10f * Time.deltaTime)
                {
                    transform.position = endPos;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(startPos, endPos, 10f * Time.deltaTime);
                }
            }
        }
        //if (introMode)
        //{
        //    if (!showingPicnic)
        //    {
        //        showPicnic -= Time.deltaTime;
        //    }
        //    if (!showingRoundabout && !showedRA)
        //    {
        //        showRoundabout -= Time.deltaTime;
        //    }
        //    else if (showingRoundabout)
        //    {
        //        lookAtPlaces -= Time.deltaTime;
        //        Vector3 dir = roundAboutPoint.position - transform.position;
        //        Quaternion lookRot = Quaternion.LookRotation(dir);
        //        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, lookAtTargetSpeed * Time.deltaTime);
        //    }
        //    if (showRoundabout <= 0)
        //    {
        //        showingRoundabout = true;
        //        showRoundabout = 2f;
        //    }
        //    if (lookAtPlaces <= 0)
        //    {
        //        showedRA = true;
        //        showingRoundabout = false;
        //        goBack = true;

        //    }
        //    if (goBack && !showingPicnic)
        //    {
        //        Vector3 dir = introDefaultRot - transform.position;
        //        Quaternion lookRot = Quaternion.LookRotation(dir);
        //        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, (lookAtTargetSpeed - 0.4f) * Time.deltaTime);
        //    }
        //    if (showPicnic <= 0)
        //    {
        //        showingPicnic = true;
        //    }
        //    if (showingPicnic)
        //    {
        //        timeAtPicnic -= Time.deltaTime;
        //        Vector3 dir = picnicPoint.position - transform.position;
        //        Quaternion lookRot = Quaternion.LookRotation(dir);
        //        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, (lookAtTargetSpeed) * Time.deltaTime);
        //    }
        //    if (timeAtPicnic <= 0)
        //    {
        //        showingPicnic = false;
        //        goBack = false;
        //        Vector3 dir = player.transform.position - transform.position;
        //        Quaternion lookRot = Quaternion.LookRotation(dir);
        //        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, (lookAtTargetSpeed) * Time.deltaTime);
        //    }
        //}
        //else
        //{
        //    if (!player.tutorialMode)
        //    {
        //        if (player.reachedTarget || player.reachedHunter)
        //        {
        //            SetAttackMode();
        //        }
        //        else if (!player.reachedTarget && !player.reachedHunter && !showHunter)
        //        {
        //            if (cam.fieldOfView >= 64)
        //            {
        //                cam.fieldOfView = 64;
        //            }
        //            else if (cam.fieldOfView != 64)
        //            {
        //                cam.fieldOfView += 8f * Time.deltaTime; //5
        //            }
        //        }
        //    }
        //}
        if (!introMode)
        {
            if (player.tutorialMode && player.transform.rotation.eulerAngles.y > 4 && player.transform.rotation.eulerAngles.y < 76)
            {
                player.tutorialText.gameObject.SetActive(true);
            }
            else if (player.tutorialMode && (player.transform.rotation.eulerAngles.y < 4 || player.transform.rotation.eulerAngles.y > 76))
            {
                player.tutorialText.gameObject.SetActive(false);
            }
            if (player.tutorialMode && Input.GetKey(KeyCode.W) && player.transform.rotation.eulerAngles.y > 4 && player.transform.rotation.eulerAngles.y < 76)
            {
                offset = flyingOffset;
                player.tutorialText.gameObject.SetActive(false);
                player.RB.useGravity = true;
                player.RB.isKinematic = false;
                player.tutorialMode = false;
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
        }
    }

    void LateUpdate()
    {
        if (!player.reachedTarget && !player.reachedHunter && !player.tutorialMode && !showHunter && !introMode)
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


            //rotation.y += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
            //rotation.x += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
            //rotation.y = Mathf.Clamp(rotation.y, -0.6f, 0.6f);
            //rotation.x = Mathf.Clamp(rotation.x, -0.6f, 0.4f);

            //Vector3 dispX = (target.rotation * Vector3.right) * rotation.y;
            //Vector3 dispY = (target.rotation * Vector3.up) * rotation.x;
            //Vector3 lookAt = target.position + dispX + dispY;
            //Vector3 dir = lookAt - camPos;
            //transform.rotation = Quaternion.LookRotation(dir.normalized, (target.rotation * Vector3.up).normalized);

            camRot = new Vector3(target.eulerAngles.x + 35f, target.eulerAngles.y, 0);
            transform.rotation = Quaternion.Euler(camRot);

            //camRot = new Vector3(target.eulerAngles.x + 35f, target.eulerAngles.y, 0);
            //Vector3 smooth = Vector3.Lerp(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z), camRot, camSpeed);
            //transform.rotation = Quaternion.Euler(smooth);

            //float smoothX = Mathf.SmoothDampAngle(transform.eulerAngles.x, target.eulerAngles.x + 35f, ref velocity.x, 0.10f);
            //float smoothY = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref velocity.y, 0.10f);
            //transform.rotation = Quaternion.Euler(new Vector3(smoothX, smoothY, 0));

        }
        if (player.tutorialMode)
        {
            Vector3 targetPos = target.position + (target.rotation * offset);
            transform.position = targetPos;

            camRot = new Vector3(target.eulerAngles.x + 10f, target.eulerAngles.y, 0);
            transform.rotation = Quaternion.Euler(camRot);
        }
    }

    void SetAttackMode()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = attackTarget.position;
        transform.position = Vector3.Lerp(startPos, endPos, 4 * Time.deltaTime);
        transform.LookAt(new Vector3(player.target.x, player.target.y - 0.2f, player.target.z));
        if (cam.fieldOfView <= 37)
        {
            cam.fieldOfView = 37;
        }
        else if (cam.fieldOfView != 37)
        {
            cam.fieldOfView -= FOVspeed * Time.deltaTime; 
        }
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

    private IEnumerator Intro()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(LookAtRA());
        yield return new WaitForSeconds(4.0f);
        StopCoroutine(LookAtRA());
        StartCoroutine(LookBack());
        yield return new WaitForSeconds(2.0f);
        StopCoroutine(LookBack());
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(LookAtPicnic());
        yield return new WaitForSeconds(3.0f);
        StopCoroutine(LookAtPicnic());
        StartCoroutine(LookAtNest());
        yield return new WaitForSeconds(4.5f);
        StopCoroutine(LookAtNest());

    }

    private IEnumerator LookAtRA()
    {
        while (true)
        {
            Vector3 dir = roundAboutPoint.position - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, lookAtTargetSpeed * Time.deltaTime);
        }
    }

    private IEnumerator LookBack()
    {
        while (true)
        {
            Vector3 dir = introDefaultRot - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, (lookAtTargetSpeed - 0.4f) * Time.deltaTime);
        }
    }

    private IEnumerator LookAtPicnic()
    {
        while (true)
        {
            Vector3 dir = picnicPoint.position - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, (lookAtTargetSpeed) * Time.deltaTime);
        }
    }
    private IEnumerator LookAtNest()
    {
        while (true)
        {
            Vector3 dir = player.transform.position - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, (lookAtTargetSpeed) * Time.deltaTime);
        }
    }



}
