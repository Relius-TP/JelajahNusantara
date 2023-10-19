using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayMovement : MonoBehaviour
{
    Pathfinding pathFinding;

    private void OnEnable()
    {
        QteSystem.OnQTEResult += StunEnemy;
    }

    private void OnDisable()
    {
        QteSystem.OnQTEResult -= StunEnemy;
    }

    private void Start()
    {
        pathFinding = gameObject.GetComponent<Pathfinding>();
    }

    private void StunEnemy(bool result)
    {
        if(result)
        {
            pathFinding.enabled = false;
            StartCoroutine(StunMovement());
        }
    }

    IEnumerator StunMovement()
    {
        Debug.Log("Enemy Stunned");
        yield return new WaitForSeconds(1.5f);
        pathFinding.enabled = true;
        Debug.Log("Enemy Move");
    }
}
