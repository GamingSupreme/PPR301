using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    //these floats handle the x and y sensativity
    public float sensX;
    public float sensY;

    //transform to handle the players orientation
    public Transform orientation;

    //floats to story the x and y of the camera
    float xRotation;
    float yRotation;

    private void Start()
    {
        //when the game starts we want the cursor to be locked in the centre
        //and not be visiable
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //we want to get the players mouse inputs in real time
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensX;

        yRotation += mouseX;
        xRotation -= mouseY;
        
        //clamp the x rotation so the player cant look further than 90 degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //first we rotate the camera to match players inputs
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //then we rotate the player to match their proper direction
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}


