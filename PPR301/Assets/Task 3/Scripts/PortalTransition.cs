using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTransition : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision){
        if (collision.gameObject.tag == "portalPlane"){
            SceneManager.LoadScene("End Screen");
        }
    }

}
