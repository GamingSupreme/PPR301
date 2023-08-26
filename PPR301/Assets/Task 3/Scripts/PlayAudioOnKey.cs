using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnKey : MonoBehaviour
{
    public AudioSource Walking_Grass;
    //public AudioSource Grapple_SFX;
    public PlayerMovement playMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Walking_Grass.isPlaying == true)
        {
            
        }
        if (Input.GetKeyDown(KeyCode.W) || 
            Input.GetKeyDown(KeyCode.A) || 
            Input.GetKeyDown(KeyCode.S) || 
            Input.GetKeyDown(KeyCode.D) && //if player is moving
            playMove.grounded == true && // if player is on the ground
            Walking_Grass.isPlaying == false) // if audio is not playing
        {
            Walking_Grass.Play();
            Debug.Log("walk audio good");
        }
        else
        {
            Walking_Grass.Stop();
        }
    }
}
