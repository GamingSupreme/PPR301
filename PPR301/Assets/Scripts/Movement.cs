using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Set player move speed
    public float moveSpeed;

    //Set Players height
    public float playerHeight;

    //Determine what is ground and what isnt
    //(Make sure to change the layer of the ground terrain to this layer)
    public LayerMask whatIsGround;

    //Used for checking whether the player can jump or not
    bool grounded;

    //To help simulate proper movement
    public float groundDrag;

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
        //and freeze its rotation
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        //Check if the player is touching the ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, whatIsGround);

        //Make sure to be checking for player input on every update frame
        PlayerInput();

        //Make sure to be always checking if the player is reaching max speed
        SpeedControl();

        //Check if the player is touching the ground, if they are apply drag
        //Otherwise dont
        if (grounded) {
            rb.drag = groundDrag;
        }else{
            rb.drag = 0;
        }  
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

        //adds force in the direction the player is facing
        rb.AddForce(inputDir.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl(){
        //Checks the Current Objects flat velocity
        Vector3 curVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //if our current velocity if higher than out move speed then were going too fast
        if (curVelocity.magnitude > moveSpeed){
            //so we calculate what out max should be
            Vector3 maxVelocity = curVelocity.normalized * moveSpeed;
            //then we sent out current velocity to the max we would like it to go at
            rb.velocity = new Vector3(maxVelocity.x, rb.velocity.y, maxVelocity.z);
        }
    }
}
