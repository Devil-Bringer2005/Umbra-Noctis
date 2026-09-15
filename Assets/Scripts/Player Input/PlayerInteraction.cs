using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public NPC npc;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (npc == null)
            return;

        npc.Interact();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
