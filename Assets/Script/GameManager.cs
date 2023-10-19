using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject QteUi;
    public GameObject QteSys;
    public GameObject SkillCheckUI;

    private void Start()
    {
        QteSys.SetActive(false);
        QteUi.SetActive(false);
        SkillCheckUI.SetActive(false);
    }

    private void Update()
    {
        if(SkillCheckController.isWaitingInput == false)
        {
            SkillCheckUI.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Player.OnCaught += GetCaught;
        Interactable.OnGetItem += GetQuestItem;
        Portal.OnSkillCheckRunning += OpeningPortal;
    }

    private void OnDisable()
    {
        Player.OnCaught -= GetCaught;
        Interactable.OnGetItem -= GetQuestItem;
        Portal.OnSkillCheckRunning -= OpeningPortal;
    }

    private void GetCaught()
    {
        Time.timeScale = 0f;
        QteUi.SetActive(true);
        QteSys.SetActive(true);
    }

    private void GetQuestItem()
    {
        SkillCheckUI.SetActive(true);
        SkillCheckController.isWaitingInput = true;
    }

    private void OpeningPortal()
    {
        SkillCheckUI.SetActive(true);
        SkillCheckController.isWaitingInput = true;
    }
}
