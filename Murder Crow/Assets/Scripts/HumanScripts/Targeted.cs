using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Targeted : MonoBehaviour
{
    public Player player;
    public Camera cam;
    public GameObject circle;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        else if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            player = GameObject.Find("Menuplayer").GetComponent<MenuScript>();
        }
        cam = Camera.main;
    }

    private void OnMouseOver()
    {
        Vector3 mousePos = -Vector3.one;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f, player.targetLayer))
        {
            if (!circle.activeInHierarchy && !player.targetIsSet && !player.reachedHunter && !player.reachedTarget && !player.reachedSkull && !player.reachedSkullNoPoint && !player.hunterSkullDropped)
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
