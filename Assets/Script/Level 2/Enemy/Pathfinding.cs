using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform startPosition;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) <= PlayerMovement.detectionRadius)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.SetDestination(startPosition.position);
        }

        
    }
}
