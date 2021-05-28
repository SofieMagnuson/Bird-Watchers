using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAnimations : MonoBehaviour
{
    public Animator anim;
    private float minBreakTime, maxBreakTime, timer;
    private bool breaks, isAttacked;

    // Start is called before the first frame update
    void Start()
    {
        minBreakTime = 4f;
        maxBreakTime = 15f;
        timer = Random.Range(minBreakTime, maxBreakTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacked)
        {
            if (anim.GetBool("isAttacked"))
            {
                anim.SetBool("isAttacked", false);
            }
            if (timer > 0 && !breaks)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)
            {
                breaks = true;
                StartCoroutine(PlayAnimation());
                timer = 1f;
            }
        }
        else
        {
            if (!anim.GetBool("isAttacked"))
            {
                FindObjectOfType<AudioManager>().Play("Gasp");
                anim.SetBool("isAttacked", true);
            }
        }
    }

    private IEnumerator PlayAnimation()
    {
        anim.SetBool("breaksIdle", true);
        yield return new WaitForSeconds(5.25f);
        anim.SetBool("breaksIdle", false);
        timer = Random.Range(minBreakTime, maxBreakTime);
        breaks = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "player")
        {
            isAttacked = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "player")
        {
            isAttacked = false;
        }
    }
}
