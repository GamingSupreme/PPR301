using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [Header("AI Variables")]
    public Transform targetObject; // Reference to ObjectB
    public float moveSpeed = 2f;
    public float bobbingAmplitude = 0.5f;
    public float bobbingFrequency = 1f; // Adjust this to control the speed of bobbing

    private Vector3 startingPosition;

    private void Start(){
        startingPosition = transform.position;
    }

    private void Update(){
        // Move towards ObjectB in the XZ plane
        Vector3 targetPosition = new Vector3(targetObject.position.x, startingPosition.y, targetObject.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Update Y position to follow ObjectB's Y position
        float bobbingOffset = bobbingAmplitude * Mathf.Sin(Time.time * bobbingFrequency);
        Vector3 newPosition = transform.position;
        newPosition.y = targetObject.position.y + bobbingOffset;
        transform.position = newPosition;
    }
}


