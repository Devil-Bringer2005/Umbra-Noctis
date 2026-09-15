using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Necromancer : MonoBehaviour
{
    public NavMeshAgent enemy;

    public Transform spawnPoint;

    public GameObject enemyBullet;
    public float enemySpeed;

    public SpriteRenderer Renderer;
    public Animator animator;
    public int maxHealth = 100;
    private int currentHealth;

    private Transform player;

    public LayerMask playerLayer;
    public Health playerHealth;
    public int damageTakenPlayer = 0;

    // Cooldown variables
    public float cooldownTime = 2f; // Time in seconds between attacks
    private float nextAttackTime = 0f; // Timestamp for the next allowed attack

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        // Check if player is within attack range
        if (distance < 2.7f)
        {
            animator.SetBool("isPatrolling", false);
            ContinuousAttack();
        }
        else
        {
            animator.SetBool("isPatrolling", true);
        }
    }

    void ContinuousAttack()
    {
        // Check if cooldown period has elapsed
        if (Time.time >= nextAttackTime)
        {
            // Trigger the attack animation if not already playing
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Necromancer_Ranged_Attack"))
            {
                animator.SetTrigger("RangedAttack");
                nextAttackTime = Time.time + cooldownTime; // Set the next attack time
            }
        }
    }

    // This method is called via an animation event
    public void FireBullet()
    {
        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.position, spawnPoint.rotation);
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        Vector3 direction = (player.position - spawnPoint.position).normalized;
        bulletRig.AddForce(direction * enemySpeed, ForceMode.Impulse);
        Destroy(bulletObj, 2f);
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

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            print("Hit");
            playerHealth.TakeDamage(damageTakenPlayer);
        }
    }

    void SpriteOff()
    {
        Renderer.enabled = false;
    }
}
