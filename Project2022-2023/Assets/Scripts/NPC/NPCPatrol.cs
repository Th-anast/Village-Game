using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    [SerializeField] private Transform[] points;
    int pointIndex;
    bool walking, waiting = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        pointIndex = -1;
        walking = false;
        animator.SetBool("Walking", walking);
        StartCoroutine(NextPoint());
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !waiting && agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            StartCoroutine(NextPoint());
        }
    }

    IEnumerator NextPoint()
    {
        walking = false;
        waiting = true;
        animator.SetBool("Walking", walking);

        //float randomSeconds = Random.Range(1, 4);
        yield return new WaitForSeconds(5);

        pointIndex = (pointIndex + 1) % points.Length;
        agent.SetDestination(points[pointIndex].position);

        walking = true;
        waiting = false;
        animator.SetBool("Walking", walking);
        yield return null;
    }
}
