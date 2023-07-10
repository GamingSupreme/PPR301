using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("Game has been closed");
            //quits the game
            Application.Quit();
        }
    }
    
}
