using System.Collections;
using UnityEngine;

public class DelayMovement : MonoBehaviour
{
    Pathfinding pathFinding;

    private void OnEnable()
    {
        QTEController.OnQTEResult += StunEnemy;
    }

    private void OnDisable()
    {
        QTEController.OnQTEResult -= StunEnemy;
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
            gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(StunMovement());
        }
        else
        {
            StartCoroutine(DisableCollider());
        }
    }

    IEnumerator DisableCollider()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    IEnumerator StunMovement()
    {
        Debug.Log("Enemy Stunned");
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Collider2D>().enabled = true;
        pathFinding.enabled = true;
        Debug.Log("Enemy Move");
    }
}
