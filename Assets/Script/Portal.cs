using System;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private int itemNeeded = 2;
    [SerializeField] private bool isFailed = false;
    public static bool skillCheckRunning = false;
    public GameObject WinUI;
    public PlayerData playerData;

    public static Action OnSkillCheckRunning;

    private void OnEnable()
    {
        SkillCheckController.OnSkillCheckResults += GetResult;
    }
    private void OnDisable()
    {
        SkillCheckController.OnSkillCheckResults -= GetResult;
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
        skillCheckRunning = true;

        OnSkillCheckRunning?.Invoke();

        if (itemNeeded == 0)
        {
            WinUI.SetActive(true);
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
