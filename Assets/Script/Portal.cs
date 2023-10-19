using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private int itemNeeded = 2;
    [SerializeField] private bool isFailed = false;
    public static bool skillCheckRunning = false;

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
        if(result == false)
        {
            isFailed = true;
        }
        skillCheckRunning = false;
    }

    public void OpenPortal()
    {
        StartCoroutine(WaitingSkillCheck());
    }

    IEnumerator WaitingSkillCheck()
    {
        isFailed = false;
        for(int i = 0; i < itemNeeded; i++)
        {
            if(isFailed)
            {
                Debug.Log("Gagal Buka Portal");
                break;
            }

            skillCheckRunning = true;
            OnSkillCheckRunning?.Invoke();
            yield return new WaitWhile(() => skillCheckRunning == true);
        }
        yield break;
    }
}
