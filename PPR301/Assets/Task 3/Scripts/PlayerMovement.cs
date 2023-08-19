using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    private float baseMoveSpeed = 7;
    public float moveSpeed;
    //value for max speed we wish to travel at
    public float maxSpeed = 12;
    public float dashSpeed;
    public float groundDrag;

    public int lives = 3;

    //How high player can jump
    public float jumpForce;
    //double jump force for the air jump
    public float doubleJumpForce;
    //jump cooldown
    public float jumpCooldown;
    //check for double jump
    public bool canDoubleJump = true;
    //bonus movespeed in the air
    public float airMultiplier;
    //checks if the player can jump
    bool readyToJump = true;
    

    [Header("Keybinds")]

    //Set our jump keybind to space
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlope;
    private RaycastHit slopeHit;


    //Reference to our orientation
    public Transform orientation;

    //storing out horizontal and vertical inputs
    float horizontalInput;
    float verticalInput;

    //bool to check if were frozen
    public bool freeze;
    //checks if were currently in grappling
    public bool activeGrapple;
    //checks if were currently in swinging
    public bool activeSwinging = false;
    //checks if were dashing
    public bool dashing = false;

    //Reference to our move direction
    Vector3 moveDir;

    //Reference to our rigidbody
    Rigidbody rb;

    //TextMeshPro variable
    public TMP_Text speedDisplay;

    private void Start(){ 
        //set this current game objects rb to our rb variable
        rb = GetComponent<Rigidbody>();
        //freeze the rb rotations so the player doesnt fall over
        rb.freezeRotation = true;
    }

    private void Update(){
        GroundCheck();
        MyInput();
        SpeedControl();
        FreezeCheck();
        CurrentSpeed();

        if (dashing){
            moveSpeed = dashSpeed;
        }

        if (grounded){
            canDoubleJump = true;
        }
        //if were grounded we need to apply ground drag to our movement
        if (grounded && !activeGrapple){
            rb.drag = groundDrag;
        }else{
            //otherwise were in the air so we have no drag
            rb.drag = 0;
        }
    }

    private void FixedUpdate(){
        MovePlayer();
    }

    private void MyInput(){
        //fetch the players current vertical and horizontal inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded){
            //Jump
            Jump();
            readyToJump = false;
            //Then start a cooldown which will set ready to jump back to true once reaching zero
            Invoke(nameof(ResetJump), jumpCooldown);
            return;
        }
        else if (Input.GetKeyDown(jumpKey) && canDoubleJump){
            DoubleJump();
            canDoubleJump = false;
        }
    }

    private void GroundCheck(){
        //now we shoot a raycast from the players body down and if it collides with an object masked as ground were grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
    }

    private void MovePlayer(){
        //if were grappling we dont want the player to mvoe
        if (activeGrapple) return;
        //first we need to calculate the direction the player wants to move in
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //if were in an active swing i want to limit player movement to a lot smaller amount
        if (activeSwinging)
        {
            rb.AddForce(moveDir.normalized * 2f * 10f * airMultiplier, ForceMode.Force);
            Debug.Log("Slow movements");
            return;
        }

        // if on slope add force in direction of slope rather than forwards
        else if (OnSLope()){
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 10f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 8f, ForceMode.Force);
        }

        //If grounded, adds force in the direction the player is facing
        else if (grounded){
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }//If were not grounded, do the same but multiply speed by our air multiplier
        else if (!grounded){
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        //turn off gravity on slopes
        rb.useGravity = !OnSLope();
    }

    private void SpeedControl()
    {
        //if were grappling we dont wanna limit our speed
        if (activeGrapple) return;
        if (activeSwinging) return;
        if (dashing) return;
        //Checks the Current Objects flat velocity
        Vector3 curVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //if our current velocity if higher than out move speed then were going too fast
        if (curVelocity.magnitude > moveSpeed && grounded){
            moveSpeed = baseMoveSpeed;
            //so we calculate what out max should be
            Vector3 maxVelocity = curVelocity.normalized * moveSpeed;
            //then we sent out current velocity to the max we would like it to go at
            rb.velocity = new Vector3(maxVelocity.x, rb.velocity.y, maxVelocity.z);
        }
        if (curVelocity.magnitude > moveSpeed && !grounded){
            moveSpeed = maxSpeed;
            //so we calculate what out max should be
            Vector3 maxVelocity = curVelocity.normalized * maxSpeed;
            //then we sent out current velocity to the max we would like it to go at
            rb.velocity = new Vector3(maxVelocity.x, rb.velocity.y, maxVelocity.z);
        }
    }

    private void Jump()
    {
        //Reset y velocity whenever going to make a jump
        //So the jump is always the same and accurate

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Apply force upwards on the player with our predetermined force
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void DoubleJump()
    {
        //Reset y velocity whenever going to make a jump
        //So the jump is always the same and accurate

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Apply force upwards on the player with our predetermined force
        rb.AddForce(transform.up * doubleJumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        //reset jump function
        readyToJump = true;
    }

    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight){
        //if we go to jump to the grapple poiint were grappling
        activeGrapple = true;

        //calculating our jump velocity
        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        //delay adding the velocity by 0.1s
        Invoke(nameof(SetVelocity), 0.1f);
    }

    private Vector3 velocityToSet;
    private void SetVelocity(){
        rb.velocity = velocityToSet;
    }

    private void FreezeCheck(){
        // if were frozen freeze our velocity
        if (freeze){
            rb.velocity = Vector3.zero;
        }
    }

    void CurrentSpeed(){
        //update ui to dispplay current speed
        speedDisplay.text = "Current Speed: " + rb.velocity.magnitude.ToString("f2");
    }

    private bool OnSLope(){
        //shoot a raycast down from the centre of the player and store the data of the object hit in the slopehit variable
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f)){
            //create a new float and store a new vector three in it with the angle being the same of the object we hit
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            // if the angle is more than 0 and less than our max slope return it
            return angle < maxSlope && angle != 0;
        }
        //otherwise return nothing
        return false;
    }

    private Vector3 GetSlopeMoveDirection(){
        return Vector3.ProjectOnPlane(moveDir, slopeHit.normal).normalized;
    }

    private void airMovement(){

    }

    /*
     * THE CODE BELOW IS TOO HARD TO EXPLAIN WITH COMMENTS ALONE SO IMMA JUST REFERENCE THE VIDEO
     * THAT HELPED ME WRITE IT, IF YOU WANNA UNDERSTAND IT WATCH THE VIDEO IT IS EXTREMELY HELPFUL
     * https://www.youtube.com/watch?v=IvT8hjy6q4o&ab_channel=SebastianLague
     */
    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight){
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }
}
