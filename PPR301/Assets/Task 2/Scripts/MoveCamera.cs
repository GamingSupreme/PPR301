using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    //public GameObject pauseMenu;

    private void Update()
    {

            transform.position = cameraPosition.position;
    }
}
