using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{

    //public bool isAtCheckpoint;
    public void StartTutorial()
    {
        //SceneManager.LoadScene("Tutorial");
    }

    public void LoadGame()

    {
        //SceneManager.LoadScene("MainScene");
        //Set the time back to 1:
        //Time.timeScale = 1;
    }

    public void QuitGame()
    {
        //Application.Quit();
    }
    public void LoadMenu()
    {

        //SceneManager.LoadScene("Credit");
    }
    public void Checkpoint()
    {
        //if (isAtCheckpoint = true) ;
        //SceneManager.LoadScene("Level 2");
        //Time.timeScale = 1f;
    }
}
