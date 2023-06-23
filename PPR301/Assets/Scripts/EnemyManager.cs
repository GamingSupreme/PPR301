using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Stats")]
    //Set some basic stats such as health, stamina ect
    public float eHealth = 30f;
    public float eStamina = 100f;

    private void Update()
    {
        //Checks if the enemies health reaches 0
        CheckForEnemy();

    }

    //Check weather the enemy has been collided with
    private void OnTriggerEnter(Collider collision)
{
        //If whatever the enemy has collided with is a weapon
        if (collision.gameObject.CompareTag("Weapon")){
            //deal damage to the enemies health
            eHealth -= 10;
        }
    }

    private void CheckForEnemy(){
        //checks if the enemies health has reached zero
        if (eHealth <= 0)
        {
            //if it has destroy the enemy
            Destroy(this.gameObject);
        }
    }
}
