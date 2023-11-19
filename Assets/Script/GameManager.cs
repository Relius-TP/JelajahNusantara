using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject QteUi;
    public GameObject QteSys;
    public GameObject SkillCheckUI;

    public PlayerData playerData;
    public GameObject keyIcon;

    private void Start()
    {
        QteSys.SetActive(false);
        QteUi.SetActive(false);
        SkillCheckUI.SetActive(false);
    }

    private void Update()
    {
        if (SkillCheckController.isWaitingInput == false)
        {
            SkillCheckUI.SetActive(false);
        }

        keyIcon.SetActive(playerData.IsHoldingKey);
    }

    private void OnEnable()
    {
        PlayerMovement.OnCaught += GetCaught;
        Interactable.OnGetItem += GetQuestItem;
        Portal.OnSkillCheckRunning += OpeningPortal;
    }

    private void OnDisable()
    {
        PlayerMovement.OnCaught -= GetCaught;
        Interactable.OnGetItem -= GetQuestItem;
        Portal.OnSkillCheckRunning -= OpeningPortal;
    }

    private void GetCaught()
    {
        QTEController.generateKey = true;
        QteUi.SetActive(true);
        QteSys.SetActive(true);
        Time.timeScale = 0f;
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
