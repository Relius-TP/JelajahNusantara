using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform startPosition;
    public static float distance;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) <= Player.detectionRadius)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.SetDestination(startPosition.position);
        }
    }
}
