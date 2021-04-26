using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MainScene");
        //Set the time back to 1:
        Time.timeScale = 1;
    }
}
