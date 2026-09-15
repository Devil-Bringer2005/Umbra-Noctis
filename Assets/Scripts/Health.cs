using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{   
    public Rigidbody rb;
    public CapsuleCollider CapsuleCollider;
    public Animator animator;
    Vector3 startPos;
    float deathDuration = 1.0f;

    public GameManagerScript gameManager;
    private bool isDead;



    public float health;
    public float maxHealth = 10;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;


    public float m_Thrust = 200f;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
        animator.SetTrigger("hit");
        OnPlayerDamaged?.Invoke();


        if (health <= 0 && !isDead)
        {   
            isDead = true;
            Die();
            //StartCoroutine(Respawn(deathDuration));
        }
    }

    void Die()
    {
        gameManager.gameOver();

        animator.SetBool("Death", true);
        rb.useGravity = false;
        CapsuleCollider.enabled = false;
        
        //Destroy(gameObject);
     
    }


    //IEnumerator Respawn(float deathDuration)
    //{
        //yield return new WaitForSeconds(deathDuration);
        //transform.position = startPos;
    //}


}
