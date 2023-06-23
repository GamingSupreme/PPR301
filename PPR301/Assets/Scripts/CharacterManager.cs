using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Stats")]
    //Set some basic stats such as health, stamina ect
    public float cHealth = 100f;
    public float cStamina = 100f;

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
        AttempAttack();
    }

    private void AttempAttack(){
        //Check for player Input
        if (Input.GetKey(Attack))
        {
            //Enable Swords collision box
            weapon.GetComponent<BoxCollider>().enabled = true;

            //play attack animation of sword
        }
    }
}
