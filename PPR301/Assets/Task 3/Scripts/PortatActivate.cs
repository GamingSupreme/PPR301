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
            if (cc.collectable1 == false)
            {
                Debug.Log("Collectable 1 has not been collected");
            }
            if (cc.collectable2 == false)
            {
                Debug.Log("Collectable 2 has not been collected");
            }
            if (cc.collectable3 == false)
            {
                Debug.Log("Collectable 3 has not been collected");
            }
            if (cc.collectable4 == false)
            {
                Debug.Log("Collectable 4 has not been collected");
            }
            if (cc.collectable5 == false)
            {
                Debug.Log("Collectable 5 has not been collected");
            }

            if (cc.collectable1 == true && cc.collectable2 == true && cc.collectable3 == true && cc.collectable4 == true && cc.collectable5 == true){
                portalPlane.gameObject.SetActive(true);
            }
        }
    }
}
