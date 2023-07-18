using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    [Header("References")]
    //references to the players components
    public Transform orientatoin;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMovementDualSwinging pm;

    [Header("Dash Variables")]
    //Variables to help control and refine the dash
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("Cooldowns")]
    //variables to control the cooldown of the dash
    public float dashCooldown;
    private float dashCooldownTimer;

    [Header("Inputs")]
    //Variable to set the ket needed to dash
    public KeyCode dashKey = KeyCode.LeftShift;

    private void Update(){
        if (Input.GetKeyDown(dashKey)){
            Dash();
        }

        if (dashCooldownTimer > 0)
            dashCooldownTimer -= Time.deltaTime;
    }

    private void Start(){
        //referencing the players rigidbody and movement script
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovementDualSwinging>();
    }

    private void Dash(){
        if (dashCooldownTimer > 0)
            return;
        else dashCooldownTimer = dashCooldown;

        //once we start dashing we need to tell our movement script were dashing
        pm.dashing = true;
        //Calculate the force we want to apply to the player to make them dash
        Vector3 forceToApply = orientatoin.forward * dashForce + orientatoin.up * dashUpwardForce;
        //Apply said force to players rigidbody, with some delay to give the movement script
        //time to swap to dashing
        delayedDashForce = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);
        //Once weve dashed we need to stop our dash after a set period of time
        Invoke(nameof(ResetDash), dashDuration);
    }

    //temp variable to store dash force
    private Vector3 delayedDashForce;

    //function to use the invoke delay
    private void DelayedDashForce(){
        rb.AddForce(delayedDashForce, ForceMode.Impulse);
    }

    private void ResetDash(){
        //once we finish dashing we need to tell our movement script were not dashing
        pm.dashing = false;
    }
}
