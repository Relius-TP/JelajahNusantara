using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BossState
{
    Stunned,
    Reset,
    Skill1,
    Skill2,
    Died,
    Win
}

public class BossMovement : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private float bossHealth = 200f;
    private float bossPrimeHealth = 200f;

    private bool qteSkillCheckStart = false;
    private BossState state;
    private int skill2Counter = 0;

    public static event Action<BossState> OnStateChanged;
    public static event Action<BossState, bool> OnPressToStart;

    private void Awake()
    {
        BossStageGameManager.OnSuccess += TakeDamage;
        PlayerBossStage.PlayerDied += PlayerDied;
    }

    private void OnDestroy()
    {
        BossStageGameManager.OnSuccess -= TakeDamage;
        PlayerBossStage.PlayerDied -= PlayerDied;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        UpdateBossState(BossState.Reset);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartQTESkillCheckSistem();
        }
    }

    private void UpdateBossState(BossState newState)
    {
        state = newState;

        switch(newState)
        {
            case BossState.Stunned:
                StartCoroutine(Stun());
                break;
            case BossState.Skill1:
                StartCoroutine(Skill1Handler());
                break;
            case BossState.Skill2:
                StartCoroutine(Skill2Handler());
                break;
            case BossState.Died:
            case BossState.Win:
                DiedHandler();
                break;
            case BossState.Reset:
                ResetStateHandler();
                break;
        }

        OnStateChanged?.Invoke(newState);
    }

    private void PlayerDied(bool obj)
    {
        if (true)
        {
            UpdateBossState(BossState.Win);
        }
    }

    private void DiedHandler()
    {
        Time.timeScale = 0f;
        StopAllCoroutines();
    }

    private void ResetStateHandler()
    {
        qteSkillCheckStart = false;
        OnPressToStart?.Invoke(state, qteSkillCheckStart);

        if(bossHealth >= 100f)
        {
            StartCoroutine(DelayAMoment(BossState.Skill1));
        }
        else if(bossHealth < 100)
        {
            StartCoroutine(DelayAMoment(BossState.Skill2));
            skill2Counter = 0;
        }
    }

    IEnumerator DelayAMoment(BossState newState)
    {
        yield return new WaitForSeconds(5f);
        UpdateBossState(newState);
    }

    IEnumerator Stun()
    {
        Vector3 targetPosition;
        yield return new WaitForSeconds(5f);

        //Pindah ke posisi awal
        targetPosition = new Vector3(0, 0, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 8f));
        UpdateBossState(BossState.Reset);
    }

    IEnumerator Skill1Handler()
    {
        Vector3 targetPosition;

        // Pergi ke kanan
        targetPosition = new Vector3(8, -4, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 5f));

        // Pergi ke kiri
        targetPosition = new Vector3(-8, -4, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 10f));

        // Pergi ke kanan
        targetPosition = new Vector3(8, -4, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 12f));

        // Pergi ke kiri
        targetPosition = new Vector3(-8, -4, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 15f));

        UpdateBossState(BossState.Stunned);
    }

    IEnumerator Skill2Handler()
    {
        Vector3 targetPosition;

        // Pergi ke kanan
        targetPosition = new Vector3(8, -4f, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 5f));

        // Pergi ke kiri
        targetPosition = new Vector3(6, 2.5f, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 30f));

        // Pergi ke kanan
        targetPosition = new Vector3(4, -4f, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 30f));

        // Pergi ke kiri
        targetPosition = new Vector3(2, 2.5f, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 30f));

        targetPosition = new Vector3(0, -4f, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 30f));

        targetPosition = new Vector3(-2, 2.5f, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 30f));

        targetPosition = new Vector3(-4, -4f, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 30f));

        targetPosition = new Vector3(-6, 2.5f, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 30f));

        targetPosition = new Vector3(-8, -4f, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition, 30f));

        if (skill2Counter <= 1)
        {
            targetPosition = new Vector3(8, -4f, 0);
            yield return StartCoroutine(MoveToPosition(targetPosition, 30f));
            skill2Counter++;
            UpdateBossState(BossState.Skill2);
        }
        else if(skill2Counter == 2)
        {
            UpdateBossState(BossState.Stunned);
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition, float time)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, time * Time.deltaTime);
            yield return null;
        }
    }

    private void TakeDamage(float damage)
    {
        bossHealth -= damage;
        healthBar.value = bossHealth / bossPrimeHealth;

        if (bossHealth <= 0)
        {
            UpdateBossState(BossState.Died);
        }
    }

    private void StartQTESkillCheckSistem()
    {
        if (state == BossState.Stunned)
        {
            qteSkillCheckStart = true;
            OnPressToStart?.Invoke(state, qteSkillCheckStart);
        }
    }

}
