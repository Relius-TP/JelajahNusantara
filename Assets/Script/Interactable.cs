using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool nearObject = false;

    private void Update()
    {
        if (nearObject)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            nearObject = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        nearObject = false;
    }
}
