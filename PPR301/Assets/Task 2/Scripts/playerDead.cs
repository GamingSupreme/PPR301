using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDead : MonoBehaviour
{
    //variable to store players location
    public Vector3 Spawnpoint;
    //reference to the player
    public GameObject player;
    //reference to the players script for movement
    public PlayerMovement pm;

    private void Start()
    {
        //on start we wanna make sure the player has a default spawn location
        Spawnpoint = player.transform.position;
    }

    private void Update()
    {
        //if the player is grounded we wanna store their location
        if (pm.grounded == true){
            Spawnpoint = player.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player falls off and hits the death box
        // we wanna check if they still have lives, if they do
        if (pm.lives > 0){
            //we take a life
            pm.lives -= 1;
            //then teleport them to their last grounded location
            player.transform.position = Spawnpoint;
        }
        else{
            //if no lives left then restart the scene
            SceneManager.LoadScene("Vertical_Slice_map");
        }
    }
}
