using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitBox : MonoBehaviour
{
    public MenuScript player;

    public void quit()
    {

        QuitGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
