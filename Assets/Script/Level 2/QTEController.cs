using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QTEController : MonoBehaviour
{
    [SerializeField] private List<GameObject> KeyPrefab;
    [SerializeField] private Transform KeyBoxUI;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private int keysNeed;
    [SerializeField] private float time;

    private QTE qte;
    private List<GameObject> spawnedArrows = new List<GameObject>();
    private QTEState qteState;

    public static event Action<QTEState> OnQTEResult;

    void Start()
    {
        qte = new QTE();
        UpdateQTEState(QTEState.Ended);
    }

    private void Update()
    {
        if (qteState == QTEState.WaitUserInput)
        {
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Space))
            {
                KeyCode pressedKey = GetPressedKey();
                bool wrongInput = qte.CheckKey(pressedKey);
                DestroySpawnedArrows();
                SetArrowUI();

                if (wrongInput)
                {
                    UpdateQTEState(QTEState.Failure);
                }
                else if (qte.IsQTEComplete())
                {
                    UpdateQTEState(QTEState.Success);
                }
            }
        }
    }

    public void StartQTEForGetCaught()
    {
        UpdateQTEState(QTEState.InProgress);
    }

    private void UpdateQTEState(QTEState state)
    {
        qteState = state;

        switch (state)
        {
            case QTEState.InProgress:
                HandleInProgressState();
                break;
            case QTEState.Success:
                HandleSuccessState();
                break;
            case QTEState.Failure:
                HandleFailureState();
                break;
        }
    }

    private void HandleInProgressState()
    {
        qte.GenerateRandomKey(keysNeed);
        SetArrowUI();
        StartCoroutine(FailedOn(time));
        UpdateQTEState(QTEState.WaitUserInput);
    }

    private void HandleSuccessState()
    {
        ResetStatus();
        OnQTEResult?.Invoke(QTEState.Success);
    }

    private void HandleFailureState()
    {
        qte.ResetList();
        ResetStatus();
        OnQTEResult?.Invoke(QTEState.Failure);
    }

    private void ResetStatus()
    {
        StopAllCoroutines();
        DestroySpawnedArrows();
        UpdateQTEState(QTEState.Ended);
        GameManager.Instance.UpdateGameState(GameState.NotCaught);
    }

    private void SetArrowUI()
    {
        for (int i = 0; i < qte.ListCount(); i++)
        {
            switch (qte.ReadList(i))
            {
                case KeyCode.UpArrow:
                    SpawnArrowGameObject(3);
                    break;
                case KeyCode.DownArrow:
                    SpawnArrowGameObject(0);
                    break;
                case KeyCode.LeftArrow:
                    SpawnArrowGameObject(1);
                    break;
                case KeyCode.RightArrow:
                    SpawnArrowGameObject(2);
                    break;
            }
        }
    }

    private void DestroySpawnedArrows()
    {
        foreach (GameObject arrow in spawnedArrows)
        {
            Destroy(arrow);
        }
        spawnedArrows.Clear();
    }

    private void SpawnArrowGameObject(int index)
    {
        var arrow = Instantiate(KeyPrefab[index]) as GameObject;
        arrow.transform.SetParent(KeyBoxUI, false);
        spawnedArrows.Add(arrow);
    }

    private KeyCode GetPressedKey()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            return KeyCode.UpArrow;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            return KeyCode.DownArrow;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            return KeyCode.LeftArrow;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            return KeyCode.RightArrow;

        return KeyCode.None;
    }

    IEnumerator FailedOn(float duration)
    {
        float timer = duration;
        while (timer > 0)
        {
            timerText.SetText(timer.ToString());
            yield return new WaitForSecondsRealtime(1f);
            timer--;
        }
        UpdateQTEState(QTEState.Failure);
    }
}
