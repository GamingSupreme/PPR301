using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Move Speed")]
    //Set player move speed
    public float moveSpeed;

    [Header("Ground Checks")]
    //Set Players height
    public float playerHeight;
    //Determine what is ground and what isnt
    //(Make sure to change the layer of the ground terrain to this layer)
    public LayerMask whatIsGround;
    //Used for checking whether the player can jump or not
    bool grounded;
    //To help simulate proper movement
    public float groundDrag;

    [Header("Jump Variables")]
    //Set how strong the players jump is
    public float jumpForce;
    //determine how often we want to let the player jump
    public float jumpCooldown;
    //If we want to make the player move faster in the air compared to on ground
    public float airMultipler;
    //Check if the player is in a state to jump
    bool readyToJump = true;

    [Header("Keybinds")]
    //Set our jump keybind to space
    public KeyCode jumpKey = KeyCode.Space;

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

        //Whenever the player presses the jump key, check to jump
        //if the player, presses space, is ready to jump and grounded
        if (Input.GetKey(jumpKey) && readyToJump && grounded){
            readyToJump = false;

            //Jump
            Jump();

            //Then start a cooldown which will set ready to jump back to true once reaching zero
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer(){
        //Again calculating the players input direction like we did
        //In the cam script
        inputDir = orientation.forward* verticalInput + orientation.right * horizontalInput;

        //If grounded, adds force in the direction the player is facing
        if (grounded){
            rb.AddForce(inputDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }//If were not grounded, do the same but multiply speed by our air multiplier
        else if (!grounded){
            rb.AddForce(inputDir.normalized * moveSpeed * 10f * airMultipler, ForceMode.Force);
        }

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

    private void Jump(){
        //Reset y velocity whenever going to make a jump
        //So the jump is always the same and accurate
        Debug.Log("Attempted Jump");

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Apply force upwards on the player with our predetermined force
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump(){
        readyToJump = true;
    }
}
