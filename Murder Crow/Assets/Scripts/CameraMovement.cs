using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public Player player;
    public Vector3 offset, flyingOffset, noMovingOffset, targetOffset;
    private float camSpeed, mouseSensitivity;
    public float tilt, maxTilt, tiltSpeed, FOV, maxFOV, FOVspeed;
    public Vector3 velocity, camRot;
    public bool attackMode;
    public Transform attackTarget1, attackTarget2, attackTarget3, attackTarget;
    public Vector2 rotation = new Vector2(0, 0);

    void Start()
    {
        camSpeed = 0.35f;
        flyingOffset = new Vector3(0.0f, 1.8f, -0.5f);
        noMovingOffset = new Vector3(0.0f, 1f, -1f);
        targetOffset = new Vector3(0.0f, 1.5f, -1f);
        offset = flyingOffset;
        velocity = Vector3.one;
        tilt = 0f;
        FOV = 45;
        FOVspeed = 3f;
        tiltSpeed = 10f;
        maxTilt = 20f;
        maxFOV = 55;
        mouseSensitivity = 0.03f;
    }

    void Update()
    {

        if (player.reachedTarget)
        {
            SetAttackMode();
        }
        else
        {
            if (cam.fieldOfView >= 64)
            {
                cam.fieldOfView = 64;
            }
            else if (cam.fieldOfView != 64)
            {
                cam.fieldOfView += 8f * Time.deltaTime; //5
            }
        }

        if (player.targetIsSet)
        {
            offset = targetOffset;
        }
        if (player.reachedTarget && Input.GetKey(KeyCode.W))
        {
            offset = flyingOffset;
        }
    }

    void LateUpdate()
    {
        if (!player.isGrounded)
        {
            if (!player.reachedTarget)
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

        }
        else
        {
            //offset = groundOffset;
            //Vector3 targetPos = target.position + (target.rotation * offset);
            //Vector3 camPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, camSpeed);
            //transform.position = camPos;
            //camRot = new Vector3(target.eulerAngles.x + 20f, target.eulerAngles.y, 0);
            //transform.rotation = Quaternion.Euler(camRot);
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
}
