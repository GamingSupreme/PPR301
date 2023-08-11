using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    //Reference to Player
    public GameObject Player;

    private void OnTriggerEnter(Collider collision){
        //Check if the collided object is the one we want to monitor
        if (collision.gameObject == Player)
        {
            //If they collide, exit the scene
            Debug.Log("COLLIDED");
            Application.Quit();
        }
    }
}
