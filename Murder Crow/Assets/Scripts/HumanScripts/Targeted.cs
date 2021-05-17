using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeted : MonoBehaviour
{
    public Player player;

    void Update()
    {
        // ändra till onMouseOver? samma som jag använder i Astar
        if (player.mouseOnTarget)
        {
            ShowTargetCircle("Prefabs/targetCircle", new Vector3(transform.position.x, transform.position.y + 1, transform.position.z));
        }
    }

    public GameObject ShowTargetCircle(string prefab, Vector3 pos)
    {
        GameObject obj = Resources.Load<GameObject>(prefab);
        GameObject instance = GameObject.Instantiate(obj);
        instance.transform.position = pos;

        return instance;
    }
}
