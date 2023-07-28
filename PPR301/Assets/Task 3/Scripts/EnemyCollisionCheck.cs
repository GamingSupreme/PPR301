using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionCheck : MonoBehaviour
{
    //Reference to Player
    public GameObject Player;

    private void OnCollisionEnter(Collision collision){
        //Check if the collided object is the one we want to monitor
        if (collision.gameObject == Player){
            //If they collide, reset the scene
            ResetScene();
        }
    }

    private void ResetScene(){
        //Reset Scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
