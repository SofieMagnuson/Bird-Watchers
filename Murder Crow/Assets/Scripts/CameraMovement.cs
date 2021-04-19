using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Player player;
    public Vector3 offset, flyingOffset, groundOffset, targetOffset;
    private float camSpeed, lookAroundSpeed;
    public float tilt, maxTilt, tiltSpeed, pitch, yaw, xRot, yRot;
    public Vector3 velocity, camRot;
    public Quaternion localRot;
    public bool isLookingAround;

    void Start()
    {
        camSpeed = 0.35f;
        flyingOffset = new Vector3(0.0f, 1.5f, -0.2f);
        groundOffset = new Vector3(0.0f, 1f, -2f);
        targetOffset = new Vector3(0.0f, 1.5f, -1f);
        offset = flyingOffset;
        velocity = Vector3.one;
        tilt = 0f;
        tiltSpeed = 10f;
        maxTilt = 20f;
        lookAroundSpeed = 100f;
        xRot = 0f;
        yRot = 0f;
    }

    void Update()
    {
        RotateView();


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
            if (!Input.GetMouseButton(0))
            {
                camRot = new Vector3(target.eulerAngles.x + 35f, target.eulerAngles.y, tilt);
                transform.rotation = Quaternion.Euler(camRot);

            }
        }
        else
        {
            offset = groundOffset;
            Vector3 targetPos = target.position + (target.rotation * offset);
            Vector3 camPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, camSpeed);
            transform.position = camPos;
            camRot = new Vector3(target.eulerAngles.x + 20f, target.eulerAngles.y, 0);
            transform.rotation = Quaternion.Euler(camRot);
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
        //    //transform.eulerAngles = new Vector3(pitch, yaw, 0f);
            //transform.eulerAngles += (lookAroundSpeed * Time.deltaTime) * new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        //}
    }

}
