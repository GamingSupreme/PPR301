using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    //pause menu
    public GameObject pauseMenu;

    private void Start()
    {
        //when the game starts we want the cursor to be locked in the centre
        //and not be visiable
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //checks to see if the pause menu is active
        if (pauseMenu.activeInHierarchy)
        {
            //do nothing cause we don't want cam to move while in pause menu
        }
        else
        {

        Debug.Log(GetComponent<Camera>().fieldOfView.ToString());
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

    public void DoFOV(float endValue, float fovChangeTime){
        //reference the camera object and set its field of view to our value
        GetComponent<Camera>().DOFieldOfView(endValue, fovChangeTime);
    }

}


