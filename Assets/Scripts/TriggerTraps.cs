using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerTraps : MonoBehaviour
{
    [SerializeField] GameObject Trap_1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Trap_1.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
