using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Player player;
    private Vector3 offset;
    private float camSpeed, tiltTime;
    public float minTilt, maxTilt;
    public Vector3 velocity, camRot;
    public Quaternion localRot;
    // max rotation åt båda hållen

    void Start()
    {
        camSpeed = 0.35f;
        offset = new Vector3(0.0f, 2.0f, -1.0f);
        velocity = Vector3.one;
        tiltTime = 2f;
        minTilt = 0f;
        maxTilt = 20f;
    }

    void Update()
    {
        RotateView();
        if (player.isGrounded)
        {
            // lägg dig rakt bakom men lägre ner
        }
    }

    void LateUpdate()
    {
        Vector3 targetPos = target.position + (target.rotation * offset);
        Vector3 camPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, camSpeed);
        transform.position = camPos;
        //if (!Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.A))
        //{
        //}
        camRot = new Vector3(target.eulerAngles.x + 35f, target.transform.eulerAngles.y, target.eulerAngles.z);
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    camRot = new Vector3(target.eulerAngles.x + 35f, target.transform.eulerAngles.y, target.eulerAngles.z - maxTilt);
        //}
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    camRot = new Vector3(target.eulerAngles.x + 35f, target.transform.eulerAngles.y, target.eulerAngles.z + maxTilt);
        //}
        transform.rotation = Quaternion.Euler(camRot);


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
        Camera cam = GetComponent<Camera>();
        transform.LookAt(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane)), Vector3.up);
    }

}
