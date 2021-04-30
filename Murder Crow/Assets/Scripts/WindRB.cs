using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindRB : MonoBehaviour
{
    public float WindStrengthMin = 0;
    public float WindStrengthMax = 5;
    public float radius = 100;

    float windStrength;
    int i;
    RaycastHit hit;
    Collider[] hitColliders;
    Rigidbody rb;

    void Update()
    {
        windStrength = Random.Range(WindStrengthMin, WindStrengthMax);

        hitColliders = Physics.OverlapSphere(transform.position, radius);

        for (i = 0; i < hitColliders.Length; i++)
        {
            if (rb = hitColliders[i].GetComponent<Rigidbody>())
                if (Physics.Raycast(transform.position, rb.position - transform.position, out hit))
                    if (hit.transform.GetComponent<Rigidbody>())
                        rb.AddForce(transform.forward * windStrength, ForceMode.Acceleration);
        }
    }
}
