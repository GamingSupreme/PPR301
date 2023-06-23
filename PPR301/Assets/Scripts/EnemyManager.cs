using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Stats")]
    //Set some basic stats such as health, stamina ect
    public float eHealth = 30f;
    public float eStamina = 100f;

    //Enemy health variable
    public TextMeshPro healthText;

    private void Update()
    {
        //Checks if the enemies health reaches 0
        CheckForEnemy();
        UpdateHealthText();

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

    void UpdateHealthText(){
        //Check to make sure Enemy is Alive
        if (this.gameObject != null)
        {
            //Update text to the enemys current health
            healthText.text = "Health: " + eHealth.ToString();

            // Position the TextMeshPro GameObject above the enemy GameObject
            Vector3 textPosition = this.gameObject.transform.position + Vector3.up * 5f;
            healthText.transform.position = textPosition;
        }
    }

     
}
