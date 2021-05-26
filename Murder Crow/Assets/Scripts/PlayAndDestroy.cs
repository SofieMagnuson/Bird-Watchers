using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayAndDestroy : MonoBehaviour
{
    VisualEffect visualEffect;
    [SerializeField] float maxLifetime = 2.5f;
    float timeStamp = 0;

    private void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
        visualEffect.Play();
        timeStamp = Time.time + maxLifetime;

    }

    private void Update()
    {
        if(Time.time >= timeStamp)
        {
            Destroy(gameObject);
        }
    }
}
