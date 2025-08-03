using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{    
    Animator animator;
    NavMeshAgent agent;
    Transform target;
    public bool running;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();        
    }

    void Update()
    {
        if (target)
        {            
            agent.SetDestination(target.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    public void SawPlayer(GameObject player)
    {
        target = player.transform;
        running = target != null;
        animator.SetBool("Running", running);
    }

    public void LostPlayer(GameObject player)
    {
        target = player.transform == target ? null : target;
        running = target != null;
        animator.SetBool("Running", running);
    }

    public void AttackPlayer()
    {
        animator.SetTrigger("Attack");
    }
}
