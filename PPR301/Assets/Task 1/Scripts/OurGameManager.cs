using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurGameManager : MonoBehaviour
{
    public int playerScore = 0;

    public void AddPoints(){
        playerScore += 10;
    }
}
