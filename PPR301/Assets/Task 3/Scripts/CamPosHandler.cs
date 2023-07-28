using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosHandler : MonoBehaviour
{
    //store where the camera should be
    public Transform cameraPosition;

    public void Update()
    {
        //move camera to where it should be
        transform.position = cameraPosition.position;
    }
}
