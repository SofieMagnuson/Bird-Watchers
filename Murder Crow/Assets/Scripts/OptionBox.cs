 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionBox : MonoBehaviour
{
    public MenuScript player;

    public void options()
    {
        OptionsScene();
    }

    public void OptionsScene()
    {
        SceneManager.LoadScene("Options");
        //Set the time back to 1:
        Time.timeScale = 1;
    }

}
