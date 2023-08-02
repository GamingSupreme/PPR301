using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCount : MonoBehaviour
{
    [Header("Collectables")]
    public bool collectable1 = false;
    public bool collectable2 = false;
    public bool collectable3 = false;
    public bool collectable4 = false;
    public bool collectable5 = false;

    private void OnCollisionEnter(Collision collision)
    {
        //Check if the player has collided with a collectable
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
    }
}
