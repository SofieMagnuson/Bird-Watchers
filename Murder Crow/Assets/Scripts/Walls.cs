using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public Transform wallCenter;
    public float pushforce;
    public float refreshRate;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            StartCoroutine(pushObject(other, true));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            StartCoroutine(pushObject(other, false));
        }
    }

    // Update is called once per frame
    IEnumerator pushObject(Collider x, bool shouldPush)
    {
        if(shouldPush)
        {
            Vector3 ForceDir = wallCenter.position - x.transform.position;
            x.GetComponent<Rigidbody>().AddForce(ForceDir.normalized * pushforce * Time.deltaTime);
            yield return refreshRate;
            StartCoroutine(pushObject(x, shouldPush));
        }
        
    }
}
