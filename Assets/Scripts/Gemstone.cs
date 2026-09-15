using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gemstone : MonoBehaviour
{   

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        
        if (playerInventory != null)
        {   
            audioManager.PlaySFX(audioManager.GemCollected);
            playerInventory.GemstoneCollected();
            gameObject.SetActive(false);
        }
    }
}
