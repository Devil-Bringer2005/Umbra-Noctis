using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    [SerializeField] private Animator DoorAnim = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                DoorAnim.Play("Door_Open",0,0.0f);
                //gameObject.SetActive(false);
            }
            else if (closeTrigger)
            {
                DoorAnim.Play("Door_Close",0,0.0f);
                //gameObject.SetActive(false);
            }


        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
