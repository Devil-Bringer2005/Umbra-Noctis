using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    Transform player;
    BoxCollider boxCollider;

    public LayerMask playerLayer;
    public Health playerHealth;
    public int damageTakenPlayer = 2;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(player.position, animator.transform.position);


        if (distance < 1.7f)
        {

            Attack();

        }
        else
        {
            animator.SetBool("isAttacking",false);
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();

        }
    }
    void Die()
    {
        animator.SetBool("Death", true);
        this.enabled = false;
        GetComponent<Collider>().enabled = false;
    }


    void Attack()
    {


        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Demon_Attack"))
        {
            animator.SetTrigger("MeleeAttack");
        }





    }

    void EnableAttack()
    {
        boxCollider.enabled = true;
    }
    void DisableAttack()
    {
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            print("Hit");
            playerHealth.TakeDamage(damageTakenPlayer);

        }
    }
}
