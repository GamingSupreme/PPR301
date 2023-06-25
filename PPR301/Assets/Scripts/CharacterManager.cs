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


    void Start()
    {
        weapon = GameObject.Find("Sword");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AttempAttack());
    }

    private IEnumerator AttempAttack(){
        //Check for player Input
        if (Input.GetKey(Attack))
        {
            //Enable Swords collision box
            animator.SetBool("IsAttacking", true);
            yield return new WaitForSeconds(0.25f);
            weapon.GetComponent<BoxCollider>().enabled = true;


            yield return new WaitForSeconds(0.35f);

            animator.SetBool("IsAttacking", false);
            weapon.GetComponent<BoxCollider>().enabled = false;

            //play attack animation of sword
        }
    }
}
