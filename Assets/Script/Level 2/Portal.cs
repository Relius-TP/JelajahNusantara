using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int itemNeeded = 4;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject portalLight;

    public static bool portalOpen = false;

    private Animator anim;

    public static event Action OnSuccess;

    private void OnEnable()
    {
        SkillCheckController.OnSkillCheckResults += GetResult;
    }
    private void OnDisable()
    {
        SkillCheckController.OnSkillCheckResults -= GetResult;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        portalLight.SetActive(false);
    }

    private void Update()
    {
        if (itemNeeded == 0)
        {
            portalLight.SetActive(true);
            anim.SetBool("Activated", true);
            portalOpen = true;
        }
    }

    private void GetResult(SkillCheckState state)
    {
        if (state == SkillCheckState.Victory && playerData.IsHoldingKey == true)
        {
            itemNeeded--;
            playerData.IsHoldingKey = false;
            OnSuccess?.Invoke();
        }
    }

    public void OpenPortal()
    {
        if (itemNeeded == 0)
        {
            SceneManager.LoadScene("LastStage");
        }
        else
        {
            GameManager.Instance.PlayerGetItem();
        }
    }
}
