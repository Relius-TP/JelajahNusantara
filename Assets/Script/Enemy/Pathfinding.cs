using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform startPosition;
    private AnimationHandler animHandler;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animHandler = GetComponent<AnimationHandler>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) <= PlayerMovement.detectionRadius)
        {
            agent.SetDestination(target.position);
            animHandler.isWalking = true;
        }
        else if (Vector2.Distance(gameObject.transform.position, startPosition.position) <= 3f)
        {
            animHandler.isWalking = false;
        }
        else
        {
            agent.SetDestination(startPosition.position);
        }

        
    }
}
