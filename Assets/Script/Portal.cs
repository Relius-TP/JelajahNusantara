using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public static bool portalOpen = false;
    [SerializeField] private int itemNeeded = 4;
    [SerializeField] private bool isFailed = false;
    public static bool skillCheckRunning = false;
    public GameObject WinUI;
    public PlayerData playerData;
    private Animator anim;

    public static Action OnSkillCheckRunning;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        SkillCheckController.OnSkillCheckResults += GetResult;
    }
    private void OnDisable()
    {
        SkillCheckController.OnSkillCheckResults -= GetResult;
    }

    private void Update()
    {
        if (itemNeeded == 0)
        {
            anim.SetBool("Activate", true);
            portalOpen = true;
        }
    }

    private void GetResult(bool result)
    {
        if(skillCheckRunning)
        {
            if (result == false)
            {
                return;
            }
            skillCheckRunning = false;
            itemNeeded--;
            playerData.IsHoldingKey = false;
        }
    }

    public void OpenPortal()
    {
        if(itemNeeded == 0)
        {
            SceneManager.LoadScene("BosStage");
        }
        else
        {
            skillCheckRunning = true;
            OnSkillCheckRunning?.Invoke();
        }
    }
}
