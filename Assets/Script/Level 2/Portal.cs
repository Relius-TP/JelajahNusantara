using UnityEngine;

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

    private void Update()
    {
        if (itemNeeded == 0)
        {
            anim.SetBool("Activate", true);
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
        skillCheckRunning = true;

        OnSkillCheckRunning?.Invoke();

        if (itemNeeded == 0)
        {
            SceneManager.LoadScene("BosStage");
        }
        else
        {
            GameManager.Instance.PlayerGetItem();
        }
    }

    //IEnumerator WaitingSkillCheck()
    //{
    //    isFailed = false;
    //    for(int i = 0; i < itemNeeded; i++)
    //    {
    //        if(isFailed)
    //        {
    //            Debug.Log("Gagal Buka Portal");
    //            break;
    //        }

    //        skillCheckRunning = true;
    //        OnSkillCheckRunning?.Invoke();
    //        yield return new WaitWhile(() => skillCheckRunning == true);
    //    }

    //    if(!isFailed)
    //    {
    //        WinUI.SetActive(true);
    //    }
    //}
}
