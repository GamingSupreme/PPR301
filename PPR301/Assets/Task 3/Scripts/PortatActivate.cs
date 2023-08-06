using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortatActivate : MonoBehaviour
{
    [Header("Variables")]
    public CollectableCount cc;
    public Transform portalPlane;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "portal")
        {
            Debug.Log("Portal Collided");

            for (int i = 0; i < 5; i++)
            {
                if (cc.collectablesArray[i] == false)
                {
                    Debug.Log("Collectable " + i + " has not been collected");
                }
            }
            

            if (cc.collectablesArray[0] == true && 
                cc.collectablesArray[1] == true && 
                cc.collectablesArray[2] == true && 
                cc.collectablesArray[3] == true && 
                cc.collectablesArray[4] == true){
                portalPlane.gameObject.SetActive(true);
            }
        }
    }
}
