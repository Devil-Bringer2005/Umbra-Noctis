using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState_Necromancer : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    float chaseRange = 4;
    float previousXPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = 3.5f;
        previousXPosition = agent.transform.position.x;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.updateRotation = false;
        agent.angularSpeed = 0;



        // Track the agent's current position
        float currentXPosition = agent.transform.position.x;

        // Flip the agent's rotation based on movement direction
        if (currentXPosition < previousXPosition)
        {
            // Moving left (x decreasing), flip to 180 degrees on Y-axis
            agent.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (currentXPosition > previousXPosition)
        {
            // Moving right (x increasing), flip to 0 degrees on Y-axis
            agent.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Update the previous X position for the next frame
        previousXPosition = currentXPosition;
        agent.SetDestination(player.position);

        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance > 6 )
        {
            animator.SetBool("isChasing", false);
        }
        //if (distance < 1.5f)
        //{
          //  animator.SetBool("isChasing", false);
        //}
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
