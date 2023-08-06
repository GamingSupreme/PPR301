using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithAI : MonoBehaviour
{
    // how far the ai can see
    public float radius;
    [Range(0, 360)]
    // how large the fov is for the ai
    public float angle;

    // to tell the ai what is the player
    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private void Start()
    {
        // sets the player refrence to the game object with the tag player
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FovRoutine());
    }

    // routine used to reduce the comp heavy check for what/who is the player
    private IEnumerator FovRoutine()
    {
        // will only look for the player 5 times per frame (if delay is = 0.2)
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        // will only check so long as the ai is alive
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }

    }

    // find player using RayCasting and SphereCasting
    private void FieldOfViewCheck()
    {
        // layerMask (targetMask) is used so that we arn't checking if every object is the player only what is within the range of the enemy radius
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        // if anyting above 0 then we found somthing
        if (rangeChecks.Length != 0)
        {
            // takes the players location and puts it in an array
            Transform target = rangeChecks[0].transform;

            // find the direction from where enemy is looking to where the player is
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // splits enemys view angle into 2 and check which segment the player is in
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                // finally determines if the enemy can see the player
                /* 
                 * How it works:
                 * 1) Start a ray cast from the center of our enemy
                 * 2) Aim raycast toward the player
                 * 3) limit that ray cast to the distance that the player is
                 * 4) and finally stop the raycast if the ray is obstructed by anything
                 */

                //if bleow check fails (view is not obstructed) then we can see the player
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        // if the player was seen by enemy before but is now out of view then reset the
        // canSeePlayer statment to false
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}
