using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditBox : MonoBehaviour
{
    public MenuScript player;

    public void credit()
    {

        CreditScene();
    }

    public void CreditScene()
    {
        SceneManager.LoadScene("CreditsScene");
        //Set the time back to 1:
        Time.timeScale = 1;
    }

}
