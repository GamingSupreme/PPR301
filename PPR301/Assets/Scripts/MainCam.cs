using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    //Setting variables to reference player
    public Transform orientation;
    public Transform playerObj;
    public Transform player;
    public Rigidbody rb;

    //Initialising speed for later use
    public float rotationSpeed;

    private void Start()
    {
        //Make Cursor invisible as we will be adding a reticle later on
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Find the direction the player is facing
        Vector3 playerDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);

        //Set our orientation to the direction for reference
        orientation.forward = playerDir.normalized;

        //Reference the directions the player is pressing
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Store the input direction
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //if the direction of the player input is not zero (nothing not moving) we want to smoothly transition
        //the camera angle towards the direction they move towards
        if (inputDir != Vector3.zero){
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }


    }
}
