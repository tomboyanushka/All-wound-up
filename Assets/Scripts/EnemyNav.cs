using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyNav : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

    //for patrolling
    public GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    public float patrolSpeed = 5.0f;


    //for chasing
    public float pursueSpeed = 10.0f;
    public GameObject target;

    public enum State
    {
        Patrol,
        Pursue
    }

    public State currentState;
    private bool alive = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentState = EnemyNav.State.Patrol;
        alive = true;

        StartCoroutine("FSM");
    }

    IEnumerator FSM()
    {
        while (alive)
        {
            switch (currentState)
            {
                case State.Patrol:
                    Patrol();
                    break;
                case State.Pursue:
                    Pursue();
                    break;
            }
            yield return null;
        }
    }
    private void Update()
    {
        
    }
    void Patrol()
    {
        //play walking animation
        agent.speed = patrolSpeed;
        if (Vector3.Distance(this.transform.position, waypoints[currentWaypointIndex].transform.position) >= 2)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].transform.position);
        }
        else if (Vector3.Distance(this.transform.position, waypoints[currentWaypointIndex].transform.position) <= 2)
        {
            currentWaypointIndex = Random.Range(0, waypoints.Length);
        }
    }

    void Pursue()
    {

    }
}
