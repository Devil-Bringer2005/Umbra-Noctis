using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : StateMachineBehaviour
{

    Transform player;

    float chaseRange = 3;

    float timer;

    List<Transform> wayPoints = new List<Transform>();

    NavMeshAgent agent;

    float previousXPosition; // To track the previous x position of the agent

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 1.5f;
        timer = 0;
        GameObject go = GameObject.FindGameObjectWithTag("WayPoints");
        foreach (Transform t in go.transform)
            wayPoints.Add(t);

        agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        previousXPosition = agent.transform.position.x; // Initialize previous x position
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.updateRotation = false;
        agent.angularSpeed = 0;

        // Check if the agent's destination has been reached
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);

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

        timer += Time.deltaTime;
        if (timer > 10)
        {
            animator.SetBool("isPatrolling", false);
        }

        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < chaseRange)
        {
            animator.SetBool("isChasing", true);
        }



    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
