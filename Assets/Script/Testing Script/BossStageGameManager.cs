using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStageGameManager : MonoBehaviour
{
    private bool qteSucces = false;
    private bool skillCheckSucces = false;
    private bool skillCheckProgress = false;

    [SerializeField] private GameObject qteUI;
    [SerializeField] private GameObject skillCheckUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject toolUI;
    [SerializeField] private GameObject textStatusUI;

    private BossState bossState;

    public static event Action<float> OnSuccess;

    private void Awake()
    {
        BossMovement.OnPressToStart += StartQTESkillCheckSistem;
        BossMovement.OnStateChanged += BossLoseWin;
        QTEHandler.OnStateChanged += QTEResultStatus;
        SkillCheckHandler.OnStateChange += SkillCheckResultStatus;
    }

    private void OnDestroy()
    {
        BossMovement.OnPressToStart -= StartQTESkillCheckSistem;
        BossMovement.OnStateChanged -= BossLoseWin;
        QTEHandler.OnStateChanged -= QTEResultStatus;
        SkillCheckHandler.OnStateChange -= SkillCheckResultStatus;
    }

    private void BossLoseWin(BossState state)
    {
        winUI.SetActive(state == BossState.Died);
        loseUI.SetActive(state == BossState.Win);
        toolUI.SetActive(state == BossState.Stunned);
    }

    private void ResetStatus()
    {
        qteSucces = false;
        skillCheckSucces = false;
    }

    private void QTEResultStatus(QTEState newState)
    {
        if(newState == QTEState.Success)
        {
            qteSucces = true;
        }
    }

    private void SkillCheckResultStatus(float value)
    {
        if (value > 0)
        {

            skillCheckSucces = true;
        }
        CheckBothResult(value);
    }

    private void CheckBothResult(float damage)
    {
        if (qteSucces && skillCheckSucces && bossState == BossState.Stunned)
        {
            ResetStatus();
            OnSuccess?.Invoke(damage);
            StartQTESkillCheckSistem(bossState, skillCheckProgress);
        }
        else if ((skillCheckSucces) && (!qteSucces) && bossState == BossState.Stunned)
        {
            ResetStatus();
            OnSuccess?.Invoke(0f);
            StartQTESkillCheckSistem(bossState, skillCheckProgress);
        }
        else if ((!skillCheckSucces || !qteSucces) && bossState == BossState.Stunned)
        {
            ResetStatus();
            OnSuccess?.Invoke(0f);
            StartQTESkillCheckSistem(bossState, skillCheckProgress);
        }
    }

    private void StartQTESkillCheckSistem(BossState state, bool active)
    {
        bossState = state;
        skillCheckProgress = active;

        QTEHandler.instance.StartQTE(state);
        SkillCheckHandler.instance.StartSkillCheck(state);

        qteUI.SetActive(skillCheckProgress);
        skillCheckUI.SetActive(skillCheckProgress);
        textStatusUI.SetActive(skillCheckProgress);
    }

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
