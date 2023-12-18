using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool interacted = false;
    public PlayerData playerData;

    [SerializeField] private AudioClip getKeyClip;

    public static event Action<AudioClip> OnSuccesGetKey;

    private void Start()
    {
        SkillCheckController.OnSkillCheckResults += GetSkillCheckResult;
    }

    private void OnDisable()
    {
        SkillCheckController.OnSkillCheckResults -= GetSkillCheckResult;
    }

    public void Interact()
    {
        interacted = true;
        if (interacted)
        {
            GameManager.Instance.PlayerGetItem();
        }
    }

    private void GetSkillCheckResult(SkillCheckState state)
    {
        if (state == SkillCheckState.Victory && interacted)
        {
            playerData.IsHoldingKey = true;
            gameObject.SetActive(false);
            OnSuccesGetKey?.Invoke(getKeyClip);
        }
    }
}
