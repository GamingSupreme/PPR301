using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTootAdioPT2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<AudioManager>().Play("TootAudio_Part 2");
    }
}
