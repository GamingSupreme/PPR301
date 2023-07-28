using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    //reference to spawn point
    public Transform spawnPoint;

    private void OnCollisionEnter(Collision collision){
        //when we collide whatever collided must be put back to the spawn point
        collision.gameObject.transform.position = spawnPoint.position;
    }
}
