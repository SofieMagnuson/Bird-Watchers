using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poopScript : MonoBehaviour
{
    public Rigidbody RB;
    public Transform targ, target8;
    public AchivementList achivementList;

    private void Start()
    {
        achivementList = GameObject.Find("AchivementList").GetComponent<AchivementList>();
        FindObjectOfType<AudioManager>().Play("Poop");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "obstacles")
        {
            RB.constraints = RigidbodyConstraints.FreezeAll;
            Destroy(this.gameObject, 4f);
        }
        if (col.gameObject.tag == "humanmust")
        {
            achivementList.ListPoopOnMan();
            RB.velocity = new Vector3(0, 0, 0);
            transform.parent = col.transform;
            Destroy(this.gameObject, 4f);
        }
        else if (col.gameObject.tag == "human")
        {
            RB.velocity = new Vector3(0, 0, 0);
            transform.parent = col.transform;
            Destroy(this.gameObject, 4f);
            FindObjectOfType<AudioManager>().Play("Oh");
        }
        else if (col.gameObject.name == "Hunter")
        {
            Destroy(this.gameObject);
            //RB.velocity = new Vector3(0, 0, 0);
            //transform.parent = col.transform;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "obstacles")
        {
            RB.constraints = RigidbodyConstraints.FreezeAll;
            Destroy(this.gameObject, 4f);
        }
    }
}
