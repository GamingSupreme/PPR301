using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [Header("AI Variables")]
    //reference to player
    public Transform targetObject;
    //Enemy move speed
    public float moveSpeed = 2f;
    //How intense the bob will be
    public float bobbingAmplitude = 0.5f;
    //how often the enemy will bob
    public float bobbingFrequency = 1f;
    private float startingY;

    private void Start(){
        startingY = transform.position.y;
    }

    private void Update(){
        // Move towards Player
        Vector3 targetPosition = new Vector3(targetObject.position.x, startingY, targetObject.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Calculate bobbing effect
        float bobbingOffset = bobbingAmplitude * Mathf.Sin(Time.time * bobbingFrequency);
        Vector3 newPosition = transform.position;
        newPosition.y = startingY + bobbingOffset;
        transform.position = newPosition;
    }
}
