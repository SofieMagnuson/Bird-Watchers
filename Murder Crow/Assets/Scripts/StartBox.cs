using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartBox : MonoBehaviour
{
    public MenuScript player;

    void Update()
    {
        LoadGame();
    }

    public void LoadGame()
    {
        //SceneManager.LoadScene("MainScene");
        //Set the time back to 1:
        //Time.timeScale = 1;
    }

}
