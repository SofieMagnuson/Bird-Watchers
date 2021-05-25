using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool restarted;
    public GameObject pauseMenuUI, ControlMenu;
 
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
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
        pauseMenuUI.SetActive(false);
        ControlMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void QuitGame()
    {      
        Application.Quit();
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    public void Restart()
    {
        restarted = true;
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }
}
