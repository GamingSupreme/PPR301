using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCount : MonoBehaviour
{
    [Header("Protal Relics")]
    public GameObject[] protalRelicArray = new GameObject[5];


    [Header("Collectables")]
    public bool[] collectablesArray = new bool[5];
    public string[] collectableTags = new string[5];

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            string tempCounter = (i + 1).ToString();
            string tagNameInput = "collectable" + tempCounter;
            collectableTags[i] = tagNameInput;
        }

        for (int i = 0; i < 5; i++)
        {
            collectablesArray[i] = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check if the player has collided with a collectable
        for (int i = 0; i < 5; i++)
        {
            if (collision.gameObject.tag == collectableTags[i])
            {
                //If they collide, tick the collectable
                collectablesArray[i] = true;
                Destroy(collision.gameObject);
                protalRelicArray[i].SetActive(true);
            }
        }
        
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