using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealth : MonoBehaviour
{
    public CharacterManager playerManager;
    float currentHealth = 0f;
    public string healthDisplay;

    public Text displayedCurrentHealth;


    // Update is called once per frame
    void Update()
    {
        // if player manager Health changes alter the current Health display to show current health level
        if (playerManager.cHealth < currentHealth || playerManager.cHealth > currentHealth)
        {
            // save the text to be displayed
            healthDisplay = "HEALTH: " + playerManager.cHealth.ToString() + " / 100";

            //Display new health level
            displayedCurrentHealth.text = healthDisplay;

            //alter current health to new level (to be used to check if a health change occured)
            currentHealth = playerManager.cHealth;
        }


    }
}
