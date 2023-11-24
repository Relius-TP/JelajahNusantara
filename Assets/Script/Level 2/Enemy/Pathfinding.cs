using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform startPosition;
    private Animator animator;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) <= PlayerMovement.detectionRadius)
        {
            agent.SetDestination(target.position);
            animator.SetBool("Walk", true);
        }
        else if (Vector2.Distance(transform.position, startPosition.position) >= 0.1)
        {
            agent.SetDestination(startPosition.position);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        
    }
}
