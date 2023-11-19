using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 2f;
    public PlayerData playerData;
    public GameObject ToolTipUI;

    private void Start()
    {
        ToolTipUI.SetActive(false);
    }

    private void Update()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactRange);
        foreach(Collider2D collie in colliderArray)
        {
            if(collie.TryGetComponent(out Interactable interactable))
            {
                ToolTipUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (!playerData.IsHoldingKey)
                    {
                        interactable.Interact();
                    }
                    else
                    {
                        Debug.Log("Cannot carry more than one key");
                    }
                } 
            }
            else if(collie.TryGetComponent(out Portal portal) )
            {
                ToolTipUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (playerData.IsHoldingKey || Portal.portalOpen)
                    {
                        portal.OpenPortal();
                    }
                    else
                    {
                        Debug.Log("No key in inventory");
                    }
                }
            }
            else
            {
                ToolTipUI.SetActive(false);
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
