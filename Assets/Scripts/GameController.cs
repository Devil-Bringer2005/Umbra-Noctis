using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector3 startPos;
    public Animator animator;

    public Health healthscript;

    public CapsuleCollider capsuleCollider;
   


    // Start is called before the first frame update
    public void Start()
    {
        startPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Dead();
    }

    void Dead()
    {
        if (healthscript.health <= 0)
        {
           
            StartCoroutine(Respawn(5f));
        }
    }

    IEnumerator Respawn(float Duration)
    {   
        yield return new WaitForSeconds(Duration);
        transform.position = startPos;
        healthscript.health = healthscript.maxHealth;
        
        //animator.SetBool("Death", false);
        //capsuleCollider.enabled = true;


    }
}
