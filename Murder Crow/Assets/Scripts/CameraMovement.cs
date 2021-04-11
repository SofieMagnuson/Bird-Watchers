using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Player player;
    private Vector3 offset;
    private float camSpeed, tiltTime, lookAroundSpeed;
    public float minTilt, maxTilt, pitch, yaw;
    public Vector3 velocity, camRot;
    public Quaternion localRot;

    void Start()
    {
        camSpeed = 0.35f;
        offset = new Vector3(0.0f, 2.0f, -1.0f);
        velocity = Vector3.one;
        tiltTime = 2f;
        minTilt = 0f;
        maxTilt = 20f;
        lookAroundSpeed = 5f;
    }

    void Update()
    {
        RotateView();
    }

    void LateUpdate()
    {
        if (!player.isGrounded)
        {
            offset = new Vector3(0.0f, 2.0f, -1.0f);
            Vector3 targetPos = target.position + (target.rotation * offset);
            Vector3 camPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, camSpeed);
            transform.position = camPos;
            //if (!Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.A))
            //{
            //}
            if (!Input.GetMouseButton(0))
            {
                camRot = new Vector3(target.eulerAngles.x + 35f, target.transform.eulerAngles.y, target.eulerAngles.z);
                transform.rotation = Quaternion.Euler(camRot);

            }
        }
        else
        {
            offset = new Vector3(0.0f, 1.0f, -1.0f);
            Vector3 targetPos = target.position + (target.rotation * offset);
            Vector3 camPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, camSpeed);
            transform.position = camPos;
            camRot = new Vector3(target.eulerAngles.x + 20f, target.transform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Euler(camRot);
        }
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    camRot = new Vector3(target.eulerAngles.x + 35f, target.transform.eulerAngles.y, target.eulerAngles.z - maxTilt);
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    camRot = new Vector3(target.eulerAngles.x + 35f, target.transform.eulerAngles.y, target.eulerAngles.z + maxTilt);
        //}


    }

    public void TiltCamera()
    {
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, maxTilt), Time.deltaTime * tiltTime);
        //    //transform.Rotate(new Vector3(0, 0, 0.1f));
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    zRot += tiltTime * Time.deltaTime;
        //    localRot = Quaternion.Euler(0.0f, 0.0f, zRot);
        //    transform.rotation = localRot;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    zRot -= tiltTime * Time.deltaTime;
        //    localRot = Quaternion.Euler(0.0f, 0.0f, zRot);
        //    transform.rotation = localRot;
        //}

        //Vector3 dir = target - transform.position;
        //dir.y = 0f;
        //Quaternion lookRot = Quaternion.LookRotation(dir);
        //transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, lookAtTargetSpeed * Time.deltaTime);

    }

    void RotateView()
    {
        //pitch += lookAroundSpeed * Input.GetAxis("Mouse Y");
        //yaw += lookAroundSpeed * Input.GetAxis("Mouse X");

        //pitch = Mathf.Clamp(pitch, 35f, 50f);
        //yaw = Mathf.Clamp(yaw, -10f, 10f);

        if (Input.GetMouseButton(0))
        {
            //transform.eulerAngles = new Vector3(pitch, yaw, 0f);
            transform.eulerAngles += lookAroundSpeed * new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        }
    }

}
