using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    //reference to our movement script
    private PlayerMovement pm;
    //reference to the main camera
    public Transform cam;
    //reference to tip of gun object
    public Transform gunTip;
    //reference to what we want to be able to grapple to
    public LayerMask Grappleable;
    //reference to line renderer
    public LineRenderer lr;

    [Header("CameraEffects")]
    public CamMovement playerCam;
    public float grappleFOV;


    [Header("Grappling Variables")]
    //the max distance our grapple will travel
    public float maxGrappleDistance;
    //delay inbetween grapples
    public float grappleDelayTime;
    //Helps us calculate the highest point in our arch of the grapple
    public float overShootYAxis;

    //reference to where our grapple currently is
    private Vector3 grapplePoint;

    [Header("Cooldowns")]
    //the cooldown for our grapple
    public float grapplingCooldown;
    //an internal timer to track grapple cooldown
    private float grapplingCooldownTimer;

    [Header("Inputs")]
    //key bind to grapple
    public KeyCode grappleKey = KeyCode.Mouse1;

    //bool to check if were currently grappling
    private bool grappling;
    


    private void Start(){
        pm = GetComponent<PlayerMovement>();
    }

    private void Update(){
        //if the grapple key is pressed try to grapple
        if (Input.GetKeyDown(grappleKey)){
            print("Attempted grapple");
            StartGrapple();
        }
            

        if (grapplingCooldownTimer > 0)
            grapplingCooldownTimer -= Time.deltaTime;
    }

    private void LateUpdate(){
        //while were grappling every update frame
        if (grappling)
            //we wanna set the start of the line renderer to our gun tip
            lr.SetPosition(0, gunTip.position);
    }

    private void StartGrapple(){
        //if our cooldown is higher than 0 then we cant grapple so exit
        if (grapplingCooldownTimer > 0){
            print("Grapple on cooldown");
            return;
        }
            
        //weve attempted to grapple so were grappling
        grappling = true;

        //before we start grpapling temporarally freeze the player
        pm.freeze = true;

        print("Shooting RayCast");

        RaycastHit hit;
        //if we shoot a ray cast our starting at our cams poisiton, and it hits an object masked as grappleable then enter
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, Grappleable)){
            //if we hit something set our grapple point to the spot we hit
            grapplePoint = hit.point;

            print("RayCast Hit");

            //then we want to start executing out grapple but with some delay first
            //(maybe play an animation here?)
            playerCam.DoFOV(grappleFOV, 0.5f);
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
            
        }else{
            print("RayCast Missed");

            //if we miss just set the grapple point to our max grapple distance and start our stop grapple function
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        //once we got to grapple, enable the line rendered
        lr.enabled = true;
        //and set the end point of the line to the grapple spot
        lr.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple(){
        
        pm.freeze = false;
        //calculates the lowest point of the player
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        //this is the difference of the lowest point of the player and the grapple point
        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        //now this is the difference of the two and adding the over shoot ontop
        float highestPointOnArc = grapplePointRelativeYPos + overShootYAxis;

        //if the grapple point is below the player then we dont need to do any of the over shoot calculations
        if (grapplePointRelativeYPos < 0)
            highestPointOnArc = overShootYAxis;

        //now that weve done our calculations we can start the grapple
        pm.JumpToPosition(grapplePoint, highestPointOnArc);

        //once weve grapple we want to stop, so we wait 1 second after the grapples been shot to stop grappling
        Invoke(nameof(StopGrapple), 1f);
    }

    private void StopGrapple(){
        //if we want to stop grappling
        // unfreeze the player
        pm.freeze = false;
        //set grappling to false
        grappling = false;
        pm.activeGrapple = false;

        //and restart the cooldown
        grapplingCooldownTimer = grapplingCooldown;

        //once we finish grappling disable the line renderer
        lr.enabled = false;
        playerCam.DoFOV(60f, 0.25f);
    }

    
}
