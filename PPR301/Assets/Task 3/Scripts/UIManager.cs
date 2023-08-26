using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //reference to the players script for movement
    public PlayerMovement pm;
    public DashScript ds;

    [Header("Jump Gameobjects")]
    public GameObject doubleJ;
    public GameObject singleJ;

    [Header("Lives Gameobjects")]
    public GameObject lives1;
    public GameObject lives2;
    public GameObject lives3;

    [Header("Energy Gameobjects")]
    public Image energyHolder;
    public Image energyJuice;

    private void Update(){

        if (pm.canDoubleJump == true){
            doubleJ.SetActive(true);
        } else if (pm.canDoubleJump == false){
            doubleJ.SetActive(false);
        }

        if (pm.grounded == true){
            singleJ.SetActive(true);
        } else if (pm.grounded == false){
            singleJ.SetActive(false);
        }

       if (pm.lives == 3){
            lives1.SetActive(true);
            lives2.SetActive(true);
            lives3.SetActive(true);
        }else if (pm.lives == 2){
            lives3.SetActive(false);
        }else if (pm.lives == 1){
            lives2.SetActive(false);
        }else if (pm.lives == 0){
            lives1.SetActive(false);
        }


       if (ds.dashCooldownTimer <= 0.1){
            energyJuice.fillAmount = 1;
        }
        else{
            energyJuice.fillAmount = 1 - ((ds.dashCooldownTimer / ds.dashCooldown) * (100 / 1)/100);
        }
        
    }
}
