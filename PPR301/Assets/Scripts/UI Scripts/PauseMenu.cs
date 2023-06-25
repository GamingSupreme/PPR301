using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    //public GameObject healthMenuUI;


    // Update is called once per frame
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

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        //healthMenuUI.SetActive(true);

        Time.timeScale = 1;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        //healthMenuUI.SetActive(false);

        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void OnClick_resumeGame()
    {
        Resume();
    }

    public void OnClick_QuitGame()
    {
        Debug.Log("Game has been closed");
    }

}
