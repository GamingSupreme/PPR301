using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortatActivate : MonoBehaviour
{
    [Header("Variables")]
    public CollectableCount cc;
    public Transform portalPlane;
    bool collectedAll = false;

    [SerializeField] private Animator myAnimationController;

    [Header("Cameras")]
    public GameObject playerCamera;
    public GameObject cutsceneCamera;

    private void Update()
    {
        for (int i = 0; i < 6; i++)
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
            cc.collectablesArray[4] == true &&
            cc.collectablesArray[5] == true &&
            collectedAll == false
            )
        {
            collectedAll = true;
            StartCoroutine(PlayAnimation());

        }
    }

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(3);
        portalPlane.gameObject.SetActive(true);
        myAnimationController.SetBool("FinishedPortal", true);
        playerCamera.SetActive(false);

        yield return new WaitForSeconds(2);
        myAnimationController.SetBool("FinishedPortal", false);
        playerCamera.SetActive(true);

    }
}
