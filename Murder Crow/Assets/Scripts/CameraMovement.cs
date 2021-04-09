using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Player player;
    private Vector3 offset;
    private float camSpeed, distanceDamp;
    public Vector3 velocity;

    void Start()
    {
        camSpeed = 3f;
        distanceDamp = 0.15f;
        offset = new Vector3(0.0f, 3.0f, -3.0f);
        velocity = Vector3.one;
    }

    void Update()
    {
        if (player.isGrounded)
        {
            // lägg dig rakt bakom men lägre ner
        }
    }

    void LateUpdate()
    {
        Vector3 targetPos = target.position + (target.rotation * offset);
        Vector3 camPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, distanceDamp);
        transform.position = camPos;
        //transform.forward = target.forward;
        
    }



}
