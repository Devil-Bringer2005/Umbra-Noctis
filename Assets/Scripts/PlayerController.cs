using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private bool isAttacking = false;


    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    Vector2 moveInput;

    public Transform HitEffectTransfrom;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    

    public int attackDamage = 20;

    public float cooldownTime = 0f;
    private bool isCooldown = false;


    // Timer or flag to control damage application
    private bool canDealDamage = false;
    AudioManager audioManager;

   
    
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving)
                {
                    if (IsRunning)
                    {
                        return runSpeed;
                    }
                    else
                    {
                        return walkSpeed;
                    }
                }
                else
                {
                    // Idle speed zero
                    return 0;
                }
            }
            else
            {
                //movement locked
                return 0;
            }
        }
    }

    [SerializeField] private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField] private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingLeft = true;
    public bool IsFacingLeft
    {
        get
        {
            return _isFacingLeft;
        }
        private set
        {
            if (_isFacingLeft != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingLeft = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    Rigidbody rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            rb.linearVelocity = Vector3.zero; // Freeze movement while attacking
        }
        else
        {
            rb.linearVelocity = new Vector3(moveInput.x * CurrentMoveSpeed, 0, moveInput.y * CurrentMoveSpeed);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (isAttacking) return; // Don't change direction while attacking

        if (moveInput.x > 0 && IsFacingLeft)
        {
            IsFacingLeft = false; // Face right
        }
        else if (moveInput.x < 0 && !IsFacingLeft)
        {
            IsFacingLeft = true; // Face left
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void RunInput(bool status) => IsRunning = status;

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && !isCooldown)
        {
           
            //StartCoroutine(Cooldown());
            audioManager.PlaySFX(audioManager.Axecut);
            animator.SetTrigger(AnimationStrings.attackTrigger);
            canDealDamage = true; // Enable damage dealing when attack animation starts
            isAttacking = true; // Prevent flipping during attack
        }
    }

    // This method will be called from an animation event or another timing control
    public GameObject hitEffectPrefab; // Assign this in the Unity Inspector

    public void ApplyAttackDamage()
    {
        if (canDealDamage)
        {
            // Detect enemies in range
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

            // Damage them
            foreach (Collider enemy in hitEnemies)
            {
                // Debug.Log("We hit " + enemy.name);
                if (enemy.CompareTag("Skeleton"))
                {
                    enemy.GetComponent<Skeleton>().TakeDamage(attackDamage);
                }
                if (enemy.CompareTag("Necromancer"))
                {
                    enemy.GetComponent<Necromancer>().TakeDamage(attackDamage);
                }
                if (enemy.CompareTag("Demon"))
                {
                    enemy.GetComponent<Demon>().TakeDamage(attackDamage);
                }

                // Instantiate the hit effect at the enemy's position
                if (hitEffectPrefab != null)
                {
                    Instantiate(hitEffectPrefab, HitEffectTransfrom.position, Quaternion.identity);
                }
            }

            canDealDamage = false; // Reset the flag after damage is applied
        }
    }


    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) { return; }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    //public void OnLatern(InputAction.CallbackContext context) 
    //{
    //    if (context.started)
    //    {
    //        // Under Development 
    //        //Need to add script and event which triggers a latern to appear , to act as a light source.
    //    }
    //}

   //private IEnumerator Cooldown()
   //{
   //     isCooldown = true;
   //     yield return new WaitForSeconds(cooldownTime);
   //     isCooldown = false;
   //}

    public void EndAttack()
    {
        isAttacking = false; // Allow flipping again after attack ends
    }

}
