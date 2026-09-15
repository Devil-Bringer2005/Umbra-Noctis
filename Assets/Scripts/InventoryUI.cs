using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{   
    private TextMeshProUGUI   gemstoneText;
   
    void Start()
    {
        gemstoneText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateGemstoneText(PlayerInventory playerInventory)
    {
        gemstoneText.text = playerInventory.NumberOfGemstones.ToString();
    }
    
}
