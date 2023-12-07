using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform startPosition;
    private Animator animator;

    private float playerDistance;

    NavMeshAgent agent;

    private void OnEnable()
    {
        PlayerVision.OnVisionRangeChange += OnVisionChangeUpdate;
    }

    private void OnDisable()
    {
        PlayerVision.OnVisionRangeChange -= OnVisionChangeUpdate;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) <= playerDistance - 0.5f)
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

    private void OnVisionChangeUpdate(float value)
    {
        playerDistance = value;
    }
}
