using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiaryBox : MonoBehaviour
{
    public MenuScript player;

    public void diary()
    {

        DiaryScene();
    }

    public void DiaryScene()
    {
        SceneManager.LoadScene("Diary");
        //Set the time back to 1:
        Time.timeScale = 1;
    }

}
