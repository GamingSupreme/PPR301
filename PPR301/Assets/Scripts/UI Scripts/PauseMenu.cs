using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject healthUI;


    // Update is called once per frame
    void Update()
    {
        //if the player presses down on the ESC or P key, it will only take the first frame of that key press and then pause the game
        // or unpause the game if the game is already paused
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
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
        //closes the heathUI and brings up the menuUI 
        pauseMenuUI.SetActive(false);
        healthUI.SetActive(true);

        // resumes the game speed back to normal
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    void Pause()
    {
        //closes the menuUI and brings up the heathUI
        pauseMenuUI.SetActive(true);
        healthUI.SetActive(false);

        // freezes the game and sets bool to say that the game is paused
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
        //quits the game
        Application.Quit();
    }

}
