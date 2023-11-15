using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 2f;
    public PlayerData playerData;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactRange);
            foreach(Collider2D collie in colliderArray)
            {
                if(collie.TryGetComponent(out Interactable interactable))
                {
                    if(!playerData.IsHoldingKey)
                    {
                        Debug.Log("Interact");
                        interactable.Interact();
                    }
                    else
                    {
                        Debug.Log("Cannot carry more than one key");
                    }
                }

                if(collie.TryGetComponent(out Portal portal) )
                {
                    if (playerData.IsHoldingKey)
                    {
                        portal.OpenPortal();
                    }
                    else
                    {
                        Debug.Log("No key in inventory");
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Menggambar lingkaran deteksi pada Scene View saat objek dipilih.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
