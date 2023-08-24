using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //when the game starts we want the cursor to be locked in the centre
        //and not be visiable
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
