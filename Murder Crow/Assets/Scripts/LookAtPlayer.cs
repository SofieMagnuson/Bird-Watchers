using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Camera cam;

    public void Update()
    {
        transform.LookAt(cam.transform.position);

    }

}
