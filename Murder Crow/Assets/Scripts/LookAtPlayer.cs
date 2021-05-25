using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    public void Update()
    {
        transform.LookAt(cam.transform.position);

    }

}
