using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AchivementList : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject AchivementListUI, greenfoot1, greenfoot2, greenfoot3, mission1, mission2, mission3, mission4, mission5, mission6, mission7, mission8, mission9, streck1, streck2, streck3;
    //public Text missionOneText, missionTwoText, missionThreeText;
    public int listNr;

    void Start()
    {
        streck1.gameObject.SetActive(false);
        streck2.gameObject.SetActive(false);
        streck3.gameObject.SetActive(false);
        greenfoot1.gameObject.SetActive(false);
        greenfoot2.gameObject.SetActive(false);
        greenfoot3.gameObject.SetActive(false);
        listNr = Random.Range(1, 4);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }
    public void Resume()
    {
        Input.GetKeyDown(KeyCode.Space);
        AchivementListUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        AchivementListUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        SetAchivementList();
    }

    public void SetAchivementList()
    {

        if (listNr == 1)
        {
            mission1.gameObject.SetActive(true);
            mission2.gameObject.SetActive(true);
            mission3.gameObject.SetActive(true);
        }
        else if (listNr == 2)
        {
            mission4.gameObject.SetActive(true);
            mission5.gameObject.SetActive(true);
            mission6.gameObject.SetActive(true);
        }
        else if (listNr == 3)
        {
            mission7.gameObject.SetActive(true);
            mission8.gameObject.SetActive(true);
            mission9.gameObject.SetActive(true);
        }
        else if (listNr == 3)
        {
            mission7.gameObject.SetActive(true);
            mission8.gameObject.SetActive(true);
            mission9.gameObject.SetActive(true);
        }
    }

    public void ListTwo()
    {
        if (listNr == 2)
        {
            Debug.Log("Poop done");
            streck1.gameObject.SetActive(true);
            greenfoot1.gameObject.SetActive(true);
        }
        
    }
    public void ListTwoTwo()
    {
        if (listNr == 2)
        {
            Debug.Log("Under Done");
            streck2.gameObject.SetActive(true);
            greenfoot2.gameObject.SetActive(true);
        }
    }
    public void ListTwoTwoTwo()
    {
        if (listNr == 2)
        {

        }
    }


}
