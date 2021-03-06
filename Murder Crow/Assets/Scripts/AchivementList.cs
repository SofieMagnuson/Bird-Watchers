using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AchivementList : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool killedMany, flewUnder, scared, pooped, killedOne, scaredTwo, poopedOnMan, killedGirl;
    public GameObject AchivementListUI, picture, sizedUp, greenfoot1, greenfoot2, greenfoot3, mission1, mission2, mission3, mission4, mission5, mission6, mission7, mission8, mission9, streck1, streck2, streck3, check1,
        check2, check3, check4, check5, check6, check7, check8, check9;
    public int listNr;
    public Animator anim;

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
    }
    public void ListKillMany()
    {
        if (listNr == 1)
        {
            FindObjectOfType<AudioManager>().Play("Writing");
            streck1.gameObject.SetActive(true);
            greenfoot1.gameObject.SetActive(true);
            check1.gameObject.SetActive(true);
            anim.Play("check");
            killedMany = true;
        }
    }
    public void ListFlyUnder()
    {
        if (listNr == 1)
        {
            FindObjectOfType<AudioManager>().Play("Writing");
            streck2.gameObject.SetActive(true);
            greenfoot2.gameObject.SetActive(true);
            check2.gameObject.SetActive(true);
            anim.Play("check");
            flewUnder = true;
        }
    }
    public void ListScare()
    {
        if (listNr == 1)
        {
            FindObjectOfType<AudioManager>().Play("Writing");
            streck3.gameObject.SetActive(true);
            greenfoot3.gameObject.SetActive(true);
            check3.gameObject.SetActive(true);
            anim.Play("check");
            scared = true;
        }
    }
    public void ListPoop()
    {
        if (listNr == 2)
        {
            FindObjectOfType<AudioManager>().Play("Writing");
            streck1.gameObject.SetActive(true);
            greenfoot1.gameObject.SetActive(true);
            check4.gameObject.SetActive(true);
            anim.Play("check");
            pooped = true;
        }
        
    }
    public void ListKillOne()
    {
        if (listNr == 2)
        {
            FindObjectOfType<AudioManager>().Play("Writing");
            streck2.gameObject.SetActive(true);
            greenfoot2.gameObject.SetActive(true);
            check5.gameObject.SetActive(true);
            anim.Play("check");
            killedOne = true;
        }
    }
    public void ListScareTwo()
    {
        if (listNr == 2)
        {

            FindObjectOfType<AudioManager>().Play("Writing");
            streck3.gameObject.SetActive(true);
            greenfoot3.gameObject.SetActive(true);
            check6.gameObject.SetActive(true);
            anim.Play("check");
            scaredTwo = true;
        }
    }

    public void ListPoopOnMan()
    {
        if (listNr == 3)
        {
            FindObjectOfType<AudioManager>().Play("Writing");
            streck1.gameObject.SetActive(true);
            greenfoot1.gameObject.SetActive(true);
            check7.gameObject.SetActive(true);
            anim.Play("check");
            poopedOnMan = true;
        }
    }
    public void ListKillGirl()
    {
        if (listNr == 3)
        {
            FindObjectOfType<AudioManager>().Play("Writing");
            streck2.gameObject.SetActive(true);
            greenfoot2.gameObject.SetActive(true);
            check8.gameObject.SetActive(true);
            anim.Play("check");
            killedGirl = true;
        }
    }
    public void ListLoseLife()
    {
        if (listNr == 3)
        {
            FindObjectOfType<AudioManager>().Play("Writing");
            streck3.gameObject.SetActive(true);
            greenfoot3.gameObject.SetActive(true);
            check9.gameObject.SetActive(true);
            anim.Play("check");
        }
    }

}
