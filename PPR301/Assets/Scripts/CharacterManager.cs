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

    //Set a variable for the animator
    public Animator animator;

    //make a reference for when the player is attacking
    private bool attacking = false;


    void Start()
    {
        //make the sword variable reference the sword object
        weapon = GameObject.Find("Sword");
    }

    // Update is called once per frame
    void Update()
    {
        //check for player attemping to attack
        StartCoroutine(AttempAttack());
    }

    private IEnumerator AttempAttack(){
        //Check for player Input
        if (Input.GetKey(Attack) && attacking == false)
        {
            //if weve entered the loop it means weve started attacking
            //and dont want to be able to enter again before we finish the loop
            attacking = true;

            //Enable Swords collision box
            animator.SetBool("IsAttacking", true);
            //wait for the animation to get to about the middle swing then enable sword hitbox (collision detection)
            yield return new WaitForSeconds(0.15f);
            weapon.GetComponent<BoxCollider>().enabled = true;

            // wait till the animation is done
            yield return new WaitForSeconds(0.20f);

            //once the animation is done tell the animator to swap back from attacking
            animator.SetBool("IsAttacking", false);
            //disable the sword hitbox so you cant just run into enemies and hit them
            weapon.GetComponent<BoxCollider>().enabled = false;
            //then give some delay before we can swing again
            yield return new WaitForSeconds(0.2f);
            attacking = false;
        }
    }
}
