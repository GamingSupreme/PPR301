using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class RedicalChanger : MonoBehaviour
{
    public GameObject reticalTarget;
    private void OnTriggerEnter(Collider other)
    {
        reticalTarget.SetActive(true);

        Debug.Log("ENTER");
        //check to 
    }


    private void OnTriggerExit(Collider other)
    {
        reticalTarget.SetActive(false);

        Debug.Log("EXIT");

    }
}
