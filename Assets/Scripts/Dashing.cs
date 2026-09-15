using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    private Animator animator;
    private TrailRenderer trailRenderer;


    [Header("Dashing")]
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 1f;
    private float dashingCooldown = 1f;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Cooldown")]
    public KeyCode dashKey = KeyCode.Space;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if(isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(dashKey))
        {
            StartCoroutine(Dash());

        }
        
        
       
    }
    //private void Dash()
    // {   
    //
    //   if(dashCdTimer>0) return;
    //   else dashCdTimer = dashCd;
    //
    //  trailRenderer.emitting = true;
    //   Vector3 forceToApply = rb.velocity * dashForce;

    //  rb.AddForce(forceToApply, ForceMode.Impulse);
    //}

    private IEnumerator Dash()
    {

        canDash = false;
        isDashing = true;
        Vector3 forceToApply = rb.linearVelocity * dashingPower;

        rb.AddForce(forceToApply, ForceMode.Impulse);
        trailRenderer.emitting = true;

        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        isDashing = false;

        yield return new WaitForSeconds(dashingTime);
        canDash = true;
        
    }      
}
