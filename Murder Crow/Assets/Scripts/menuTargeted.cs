using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuTargeted : MonoBehaviour
{

    public MenuScript menuPlayer;
    public Camera cam;
    public GameObject circle;

    void Start()
    {
        menuPlayer = GameObject.Find("Menuplayer").GetComponent<MenuScript>();
        cam = Camera.main;
    }

    private void OnMouseOver()
    {
        Vector3 mousePos = -Vector3.one;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f, menuPlayer.targetLayer))
        {
            if (!circle.activeInHierarchy && !menuPlayer.targetIsSet && !menuPlayer.reachedTarget)
            {
                circle.SetActive(true);
            }
        }
        else
        {
            if (circle.activeInHierarchy)
            {
                circle.SetActive(false);
            }
        }
    }

    private void OnMouseExit()
    {
        if (circle.activeInHierarchy)
        {
            circle.SetActive(false);
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
