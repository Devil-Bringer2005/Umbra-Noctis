using System.Collections;
using UnityEngine;

public class Dasher : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 500f;
    private float dashingTime = 10f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody rb;
    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        //horizontal = Input.GetAxisRaw("Horizontal");

        //if (Input.GetButtonDown("Jump") && IsGrounded())
        //{
            //rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        //}

        //if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        //{
          //  rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        //}

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }

        //Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.linearVelocity = new Vector3(horizontal * speed, rb.linearVelocity.y);
    }

    //private bool IsGrounded()
    //{
      //  return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    //}

    

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        
        rb.linearVelocity = new Vector3(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
       
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}