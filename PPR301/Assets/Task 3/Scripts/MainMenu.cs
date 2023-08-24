using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Pannels")]
    public GameObject StartPannel;
    public GameObject OptionsPannel;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    public void PlayGame()
    {
        SceneManager.LoadScene("Vertical_Slice_map");
    }

    public void OptionsMenu()
    {
        //Set options menu to true
        //set start pannel to false
    }

    public void ExitGame()
    {
        // Quits the game
        QuitGame();
    }

    public void RestartGame()
    {
        // Quits the game
        SceneManager.LoadScene("Start menu");
    }

    public void Update()
    {
        // When the escape key is pressed -> exit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Quits the game
            QuitGame();
        }
    }


    //=====================================================================================================================
    //=====================================================================================================================

    // Functions

    void QuitGame()
    {
        Debug.Log("Game has been closed");
        Application.Quit();
    }
    


}
