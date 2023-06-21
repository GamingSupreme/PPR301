using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Stats")]
    //Set some basic stats such as health, stamina ect
    public float health = 100f;
    public float stamina = 100f;

    [Header("Keybinds")]
    //Set our jump keybind to space
    public KeyCode Attack = KeyCode.Mouse0;

    //Reference players weapon
    public GameObject weapon;


    void Start()
    {
        weapon = GameObject.Find("Sword");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AttempAttack(){
        //Check for player Input
        if (Input.GetKey(Attack))
        {
            //Enable Swords collision box
            weapon.GetComponent<BoxCollider>().enabled = true;

            //play attack animation of sword


            //check if sword collides with an object

        //check if that object is an enemy by tag
        //if yes deal damage, if not ignore the collision
        }
    }
}
