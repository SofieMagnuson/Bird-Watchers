using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartBox : MonoBehaviour
{
    public MenuScript player;

    public void start()
    { 
            LoadGame();
    }
            
    public void LoadGame()
    {
        SceneManager.LoadScene("Tutorial");
        //Set the time back to 1:
        //Time.timeScale = 1;
    }

}
