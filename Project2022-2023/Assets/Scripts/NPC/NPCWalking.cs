using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWalking : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] private Transform[] points;
    int pointIndex;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        pointIndex = -1;
        NextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance) {
            NextPoint();
        }
    }

    void NextPoint()
    {
        pointIndex = (pointIndex + 1) % points.Length;
        agent.SetDestination(points[pointIndex].position);
    }
}
