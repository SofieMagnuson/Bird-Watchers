using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    private float camSpeed, tiltTime;
    public float zRot;
    public Vector3 velocity;
    public Quaternion localRot;
    // max rotation åt båda hållen

    void Start()
    {
        camSpeed = 0.35f;
        offset = new Vector3(0.0f, 4.0f, -4.0f);
        velocity = Vector3.one;
        tiltTime = 5f;
        zRot = 0f;
    }

    void LateUpdate()
    {
        Vector3 targetPos = target.position + (target.rotation * offset);
        Vector3 camPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, camSpeed);
        transform.position = camPos;
        Vector3 camRot = new Vector3(target.eulerAngles.x + 30f, target.transform.eulerAngles.y, target.eulerAngles.z);
        transform.rotation = Quaternion.Euler(camRot);


    }

    public void TiltCamera()
    {

        if (Input.GetKey(KeyCode.D))
        {
            zRot += tiltTime * Time.deltaTime;
            localRot = Quaternion.Euler(0.0f, 0.0f, zRot);
            transform.rotation = localRot;
        }
        if (Input.GetKey(KeyCode.A))
        {
            zRot -= tiltTime * Time.deltaTime;
            localRot = Quaternion.Euler(0.0f, 0.0f, zRot);
            transform.rotation = localRot;
        }

    }

}
