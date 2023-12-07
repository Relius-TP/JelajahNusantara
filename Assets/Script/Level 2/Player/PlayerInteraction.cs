using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Vector3 Yoffset;

    public float interactRange = 2f;
    public PlayerData playerData;
    public GameObject ToolTipUI;
    public GameObject keyIcon;

    public LayerMask interactableLayer;

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
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, interactRange, interactableLayer);

        foreach (Collider2D collie in colliderArray)
        {
            float distanceToInteractable = Vector2.Distance(transform.position, collie.transform.position);

            if (distanceToInteractable <= interactRange && collie.enabled == true)
            {
                if (collie.TryGetComponent(out Interactable interactable))
                {
                    SetToolTipActive(true, collie.transform);
                    HandleInteractable(interactable);
                }
                else if (collie.TryGetComponent(out Portal portal))
                {
                    SetToolTipActive(true, collie.transform);
                    HandlePortal(portal);
                }
                else
                {
                    SetToolTipActive(false);
                }
            }
            else
            {
                SetToolTipActive(false);
            }
        }
    }

    private void HandleInteractable(Interactable interactable)
    {
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

    private void SetToolTipActive(bool isActive, Transform objectPosition = null)
    {
        if (objectPosition != null)
        {
            ToolTipUI.GetComponent<RectTransform>().position = objectPosition.position + Yoffset;
        }

        ToolTipUI.SetActive(isActive);
    }
}
