using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int itemNeeded = 4;
    [SerializeField] private PlayerData playerData;

    public static bool portalOpen = false;

    private Animator anim;

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

    private void Update()
    {
        if (itemNeeded == 0)
        {
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
        }
    }

    public void OpenPortal()
    {
        if (itemNeeded == 0)
        {
            SceneManager.LoadScene("BosStage");
        }
        else
        {
            GameManager.Instance.PlayerGetItem();
        }
    }
}
