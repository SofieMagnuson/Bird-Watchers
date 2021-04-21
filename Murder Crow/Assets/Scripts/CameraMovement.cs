using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public Player player;
    public Vector3 offset, flyingOffset, noMovingOffset, targetOffset;
    public Vector2 mouseDir, smoothing, result;
    private float camSpeed, lookAroundSpeed, mouseSensitivity, drag;
    public float tilt, maxTilt, tiltSpeed, pitch, yaw, xRot, yRot, FOV, maxFOV, FOVspeed;
    public Vector3 velocity, camRot;
    public Quaternion localRot;
    public bool isLookingAround, attackMode;
    public Transform attackTarget1, attackTarget2, attackTarget3, attackTarget;
    Vector2 rotation = new Vector2(0, 0);

    void Start()
    {
        camSpeed = 0.35f;
        flyingOffset = new Vector3(0.0f, 1.2f, -0.2f);
        noMovingOffset = new Vector3(0.0f, 1f, -1f);
        targetOffset = new Vector3(0.0f, 1.5f, -1f);
        offset = flyingOffset;
        velocity = Vector3.one;
        tilt = 0f;
        FOV = 45;
        FOVspeed = 10f;
        tiltSpeed = 10f;
        maxTilt = 20f;
        maxFOV = 55;
        lookAroundSpeed = 100f;
        xRot = 0f;
        yRot = 0f;
        mouseSensitivity = 2.5f;
        drag = 1.5f;
    }

    void Update()
    {
        RotateView();

        if (player.reachedTarget)
        {
            SetAttackMode();
        }
        else
        {
            if (cam.fieldOfView >= 50)
            {
                cam.fieldOfView = 50;
            }
            else if (cam.fieldOfView != 50)
            {
                cam.fieldOfView += 5f * Time.deltaTime;
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
                if (Input.GetKey(KeyCode.D))
                {
                    tilt = Mathf.Max(tilt - tiltSpeed * Time.deltaTime, -maxTilt);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    tilt = Mathf.Min(tilt + tiltSpeed * Time.deltaTime, maxTilt);
                } else if (tilt != 0)
                {
                    tilt = tilt < 0 ? Mathf.Min(tilt + tiltSpeed * 2 * Time.deltaTime, 0) : Mathf.Max(tilt - tiltSpeed * 2 * Time.deltaTime, 0);
                }

                Vector3 targetPos = target.position + (target.rotation * offset);
                Vector3 camPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, camSpeed);
                transform.position = camPos;

                camRot = new Vector3(target.eulerAngles.x + 35f, target.eulerAngles.y, tilt);
                transform.rotation = Quaternion.Euler(camRot);
            }


            //mouseDir = new Vector2(Input.GetAxisRaw("Mouse X") * mouseSensitivity, Input.GetAxisRaw("Mouse Y") * mouseSensitivity);
            //smoothing = Vector2.Lerp(smoothing, mouseDir, 1 / drag);
            //result += smoothing;
            //result.y = Mathf.Clamp(result.y, -40, 40);
            //result.x = Mathf.Clamp(result.x, -40, 40);
            //transform.localRotation = Quaternion.AngleAxis(-result.y, Vector3.right) * Quaternion.AngleAxis(-result.x, Vector3.up);


            //rotation.y += Input.GetAxisRaw("Mouse X");  // kolla om man kan få targets lokala axis
            //rotation.x += -Input.GetAxisRaw("Mouse Y");
            //transform.eulerAngles = rotation * mouseSensitivity;

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


    void RotateView()
    {
        //pitch += lookAroundSpeed * Input.GetAxis("Mouse Y");
        //yaw += lookAroundSpeed * Input.GetAxis("Mouse X");

        //pitch = Mathf.Clamp(pitch, 35f, 50f);
        //yaw = Mathf.Clamp(yaw, -10f, 10f);
        //isLookingAround = true;

        //float mouseX = Input.GetAxis("Mouse X") * lookAroundSpeed * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * lookAroundSpeed * Time.deltaTime;

        //xRot -= mouseY;
        //xRot = Mathf.Clamp(xRot, 0, 70);
        //yRot += mouseX;
        //yRot = Mathf.Clamp(yRot, -35, 35);

        //transform.localRotation = Quaternion.Euler(xRot, yRot, 0f);
        //transform.Rotate(Vector3.up * mouseX * mouseY);

        //if (Input.GetMouseButton(0))
        //{
        //    transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        //    //transform.eulerAngles += (lookAroundSpeed * Time.deltaTime) * new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        //}
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
            cam.fieldOfView -= 3f * Time.deltaTime; 
        }
    }
}
