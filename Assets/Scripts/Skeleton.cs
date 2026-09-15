using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    public SpriteRenderer Renderer;
    Transform player;
    BoxCollider boxCollider;

    public Transform HitTransform;

    public LayerMask playerLayer;
    public Health playerHealth;
    public float damageTakenPlayer = 2;

    public int poise = 3;
    private int originalPoise;

    public float Knockspeed = 100f;
    //public float coolDown;
    //float lastAttacked;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boxCollider = GetComponentInChildren<BoxCollider>();
        originalPoise = poise; // Store the original poise value
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, animator.transform.position);

        if (distance < 1f)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        audioManager.PlaySFX(audioManager.SkeletonHit);

        poise -= 1;

        if (poise <= 0)
        {
            animator.SetTrigger("Hit");
            poise = originalPoise; // Reset poise to its original value
        }

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
        Destroy(gameObject, 5f);
    }

    void Attack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack"))
        {
            animator.SetTrigger("meleeAttack");
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
            //print("Hit");
            playerHealth.TakeDamage(damageTakenPlayer);
        }
    }
}
