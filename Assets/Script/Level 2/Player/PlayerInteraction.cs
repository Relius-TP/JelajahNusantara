using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 2f;
    public PlayerData playerData;
    public GameObject ToolTipUI;
    public GameObject keyIcon;

    private void Start()
    {
        SetToolTipActive(false);
    }

    private void Update()
    {
        CheckInteractions();
        keyIcon.SetActive(playerData.IsHoldingKey);
    }

    private void CheckInteractions()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactRange);

        foreach (Collider2D collie in colliderArray)
        {
            if (collie.TryGetComponent(out Interactable interactable))
            {
                HandleInteractable(interactable);
            }
            else if (collie.TryGetComponent(out Portal portal))
            {
                HandlePortal(portal);
            }
            else
            {
                SetToolTipActive(false);
            }
        }
    }

    private void HandleInteractable(Interactable interactable)
    {
        SetToolTipActive(true);

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

    private void HandlePortal(Portal portal)
    {
        SetToolTipActive(true);

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

    private void SetToolTipActive(bool isActive)
    {
        ToolTipUI.SetActive(isActive);
    }

    private void OnDrawGizmos()
    {
        // Menggambar lingkaran deteksi pada Scene View saat objek dipilih.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
