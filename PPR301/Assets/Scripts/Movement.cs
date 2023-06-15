using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Set player move speed
    public float moveSpeed;

    //Reference players orientation
    public Transform orientation;

    //Reference both inputs
    float horizontalInput;
    float verticalInput;

    //Reference the players direction
    Vector3 inputDir;

    //reference players rigidbody
    Rigidbody rb;

    private void Start(){
        //On scene start we reference this objects rigid body to out variable
        // and freeze its rotation
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        //Make sure to be checking for player input on every update frame
        PlayerInput();
    }

    private void FixedUpdate()
    {
        //Make sure to move player every frame
        MovePlayer();
    }

    private void PlayerInput(){
        //Get the players inputs and store them in our variables
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer(){
        //Again calculating the players input direction like we did
        //In the cam script
        inputDir = orientation.forward* verticalInput + orientation.right * horizontalInput;

        rb.AddForce(inputDir.normalized * moveSpeed * 10f, ForceMode.Force);
    }


}
