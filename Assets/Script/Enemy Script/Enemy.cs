using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    public static float distance;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if ( distance <= 5f)
        {
            agent.SetDestination(target.position);
        }
        else if (distance <= 3f)
        {
            Debug.Log("Range Attack");
        }
    }
}
