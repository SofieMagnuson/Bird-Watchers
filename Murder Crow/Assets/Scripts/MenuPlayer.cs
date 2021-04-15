using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPlayer : MonoBehaviour
{
    public MenuScript player;

    void Update()
    {
        if (!player.reachedTarget)
        {
            QuitGame();
        }
    }

    public void QuitGame()
    {
        //Application.Quit();
    }

}
