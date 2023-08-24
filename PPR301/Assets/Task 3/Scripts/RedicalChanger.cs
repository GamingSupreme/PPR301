using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class RedicalChanger : MonoBehaviour
{
    public float CastRange = 5f;
    public GameObject UI_Aimer;

    public void Update()
    {
        Vector3 direction = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * CastRange));
        
        //draw ray for us to see in scene screen
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * CastRange));

        if (Physics.Raycast(theRay, out RaycastHit hit, CastRange))
        {
            if (hit.collider.tag == "Untagged")
            {
                UI_Aimer.SetActive(true);
                print("its a grappleable object");
            }
        }
        else
        {
            UI_Aimer.SetActive(false);
        }
    }

}

    /*
    //public GameObject reticalTarget;
    //private void OnTriggerEnter(Collider other)
    //{
    //    //if (other.gameObject.layer == 6)
    //    //{
    //        reticalTarget.SetActive(true);
    //    //}
    //    Debug.Log("ENTER");
    //    //check to 
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    // other.gameObject.layer == 6 &&
    //    if (reticalTarget.activeInHierarchy == false)
    //    {
    //        reticalTarget.SetActive(true);
    //    }
    //    Debug.Log("STAY");
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    reticalTarget.SetActive(false);

    //    Debug.Log("EXIT");

    //}
    */