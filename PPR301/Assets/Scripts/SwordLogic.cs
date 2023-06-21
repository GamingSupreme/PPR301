using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLogic : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the game object has collided with something
        if (collision.gameObject.CompareTag("OtherObject"))
        {
            Debug.Log("Collision detected with OtherObject");
            // Perform desired actions
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the game object has triggered a collision with something
        if (other.gameObject.CompareTag("OtherTrigger"))
        {
            Debug.Log("Trigger collision detected with OtherTrigger");
            // Perform desired actions
        }
    }
}
