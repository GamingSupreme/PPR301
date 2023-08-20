using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableCount : MonoBehaviour
{
    public TextMeshProUGUI collectableCounter;

    [SerializeField] private Animator myAnimationController;

    public GameObject UI;

    [Header("Cameras")]
    public GameObject playerCamera;
    public GameObject cutsceneCamera;

    [Header("Protal Relics")]
    public GameObject[] protalRelicArray = new GameObject[6];
    public int numberOfRelicsRemaining = 6;


    [Header("Collectables")]
    public bool[] collectablesArray = new bool[6];
    public string[] collectableTags = new string[6];


    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            string tempCounter = (i + 1).ToString();
            string tagNameInput = "collectable" + tempCounter;
            collectableTags[i] = tagNameInput;
        }

        for (int i = 0; i < 6; i++)
        {
            collectablesArray[i] = false;
        }

        collectableCounter.text = "Relics left: 6";
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if the player has collided with a collectable
        for (int i = 0; i < 6; i++)
        {
            if (collision.gameObject.tag == collectableTags[i])
            {
                Debug.Log("I COLLIDED AND DESTROYED A COLLECTABLE");
                //If they collide, tick the collectable
                collectablesArray[i] = true;
                Destroy(collision.gameObject);
                protalRelicArray[i].SetActive(true);
                numberOfRelicsRemaining--;
                collectableCounter.text = "Relics left: " + (numberOfRelicsRemaining.ToString());

                
                StartCoroutine(PlayAnimation(i));
            }
        }
        
    }

    IEnumerator PlayAnimation(int i)
    {
        UI.SetActive(false);
        cutsceneCamera.SetActive(true);
        myAnimationController.SetBool("Play" + (i + 1).ToString(), true);
        playerCamera.SetActive(false);
        
        yield return new WaitForSeconds(2);
        myAnimationController.SetBool("Play" + (i + 1).ToString(), false);
        playerCamera.SetActive(true);
        cutsceneCamera.SetActive(false);
        UI.SetActive(true);
    }

}

/*
        if (collision.gameObject.tag == "collectable1")
        {
            //If they collide, tick the collectable
            collectable1 = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "collectable2")
        {
            //If they collide, tick the collectable
            collectable2 = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "collectable3")
        {
            //If they collide, tick the collectable
            collectable3 = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "collectable4")
        {
            //If they collide, tick the collectable
            collectable4 = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "collectable5")
        {
            //If they collide, tick the collectable
            collectable5 = true;
            Destroy(collision.gameObject);
        }
        */