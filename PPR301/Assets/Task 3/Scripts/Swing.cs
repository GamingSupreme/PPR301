using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Swing : MonoBehaviour
{
    [Header("Inputs")]
    //ket set to swing key
    public KeyCode swingKey = KeyCode.Mouse0;

    [Header("References")]
    //reference to line renderer
    public LineRenderer lr;
    //Refernce to player in scene
    public Transform player;
    //reference to the main camera
    public Transform cam;
    //reference to tip of gun object
    public Transform gunTip;
    //reference to what we want to be able to grapple to
    public LayerMask Grappleable;
    //reference to movement script
    public PlayerMovement pm;

    [Header("Swinging")]
    //how far away we can attach swing from
    private float maxSwingDistance = 25f;
    //reference to our swing point
    private Vector3 swingPoint;
    //reference to our joint
    private SpringJoint joint;
    public float swingSpeed;

    private float currentAirTime = 0;
    private float lastAirTime;

    public Vector3 currentGrapplePosition;

    //TextMeshPro variable
    public TMP_Text currentAirTimeDisplay;
    public TMP_Text lastAirTimeDisplay;


    private void Update(){
        //check if were swinging
        SwingCheck();

        //if swing key is held down, swing
        if (Input.GetKeyDown(swingKey))
        {
            StartSwing();

            
        }
            

        // if swing key is release, stop swing
        if (Input.GetKeyUp(swingKey))
            StopSwing();
    }

    private void LateUpdate(){
        DrawRope();
    }

    private void SwingCheck(){
        //if we have have a joint then we are swinging
        if (joint == null)
            pm.activeSwinging = false;
        else{
            currentAirTime += Time.deltaTime;
            currentAirTimeDisplay.text = "Current Air Time: " + currentAirTime.ToString("f2");
        }
            
    }

    void DrawRope(){
        //dont draw the rope if were not swinging
        if (!joint)
            return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        //draw line from gun tip to swing point
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }

    private void StartSwing(){
        //when we swing set swinging to true
        pm.activeSwinging = true;

        RaycastHit hit;

        //while swinging make sure we move at our swing speed and not move speed
        pm.moveSpeed = swingSpeed;
        //shoot a raycast from the players cam forward at a distance of our max swing distance
        //then check if it connects with a terrain mark with "Grappleable"
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, Grappleable)){
            //if we hit terrarian we want to
            //set our swing point to where the raycast hit
            swingPoint = hit.point;
            //add a joint to the player
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            //and anchor the joint to the swing point
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            //the length of the grapple and the distance of the player from the grapple point
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.maxDistance = distanceFromPoint * 0.25f;

            //variables to customise to fit our game
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }

    void StopSwing(){
        lr.positionCount = 0;
        Destroy(joint);
        lastAirTime = currentAirTime;
        currentAirTime = 0;
        currentAirTimeDisplay.text = "Current Air Time: " + currentAirTime.ToString("f2");
        lastAirTimeDisplay.text = "Last Air Time: " + lastAirTime.ToString("f2") + "!";
    }    
}
