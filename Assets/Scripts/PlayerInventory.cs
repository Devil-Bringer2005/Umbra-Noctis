using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfGemstones { get; private set; }

    public UnityEvent<PlayerInventory> OnGemstoneCollected;
    public void GemstoneCollected()
    {
        NumberOfGemstones++;
        OnGemstoneCollected.Invoke(this);
    }
    

}
