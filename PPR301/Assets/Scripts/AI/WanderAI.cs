using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAI : MonoBehaviour
{
    public FieldOfView fieldOfView_Script;

    public float wanderSpeed = 3f;
    public float rotationSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    //Set a variable for the animator
    public Animator animator;

    public Transform player; // Reference to the player's transform
    private float distance; // Distance between enemy and player
    private float direction; // Direction from enemy to player in degrees
    private Quaternion targetRotation; // Target rotation to face the player
    private float movementSpeed = 3f; // Movement speed of the enemy
    private float stoppingDistance = 1.5f; // Distance to stop from the player


    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Will only allow AI to Wander if AI doesn't see the player and is not wandering at the moment
        if (isWandering == false && fieldOfView_Script.canSeePlayer == false)
        {
            StopCoroutine(ChasePlayer());
            StartCoroutine(Wander());
        }
        else if (fieldOfView_Script.canSeePlayer == true)
        {
            StopCoroutine(Wander());
            
            // Idiot proofing so that the wander and chase Coroutines conflicts
            isWalking = false;
            isWandering = false;
            isWandering = false;
            isRotatingRight = false;
            isRotatingLeft = false;

            StartCoroutine(ChasePlayer());
            //TODO: StartCoroutine(ChasePlayer());
            //Enemy will chase player

        }

        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
        }
        else if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
        }

        if (isWalking == true)
        {
            
            transform.position += transform.forward * wanderSpeed * Time.deltaTime;
        }
    }

    IEnumerator Wander()
    {
        // this is for howlong the ai will rotate for
        int rotationTime = Random.Range(1, 3);

        // the amount of time between rotations
        int rotateWait = Random.Range(3, 4);

        //randomly picks if ai will rotate left or right
        int rotate_L_or_R = Random.Range(1, 2);

        //how long ai will wait before walk again
        int walkWait = Random.Range(1, 4);

        //how long before ai will walk for for
        int walkTime = Random.Range(1, 5);

           isWandering = true;

        //will take the random number and wait that many seconds
        yield return new WaitForSeconds(walkWait);

            isWalking = true;
            animator.SetBool("EnemyWalking", true);

        //will walk for the given amount of random seconds
        yield return new WaitForSeconds(walkTime);

            // AI has stoped walking
            isWalking = false;
            animator.SetBool("EnemyWalking", false);

        yield return new WaitForSeconds(rotateWait);

        // if LorR == 1 (rotate Right)
        // if LorR == 2 (rotate Left)
        if (rotate_L_or_R == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingRight = false;
        }
        else if (rotate_L_or_R == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingLeft = false;
        }

        //Enemy AI has finnished wandering cycle
        isWandering = false;
    }

    IEnumerator ChasePlayer()
    {

        CalculateDistance();
        CalculateDirection();

        RotateTowardsPlayer();

        if (distance > stoppingDistance)
        {
            MoveTowardsPlayer();
        }
        
        yield return 0;
    }

    void CalculateDistance()
    {
        // Calculate distance between enemy and player
        distance = Vector3.Distance(transform.position, player.position);
        Debug.Log("Distance = " + distance);
    }

    void CalculateDirection()
    {
        // Calculate direction from enemy to player in degrees
        Vector3 directionVector = (player.position - transform.position).normalized;
        direction = Mathf.Atan2(directionVector.x, directionVector.z) * Mathf.Rad2Deg;
        Debug.Log("Direction = " + direction);
    }
    void RotateTowardsPlayer()
    {
        // Rotate the enemy towards the player
        targetRotation = Quaternion.Euler(0f, direction, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void MoveTowardsPlayer()
    {
        // Move the enemy towards the player
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }
}

