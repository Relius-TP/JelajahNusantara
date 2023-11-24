using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QTEHandler : MonoBehaviour
{
    public static QTEHandler instance;

    [SerializeField] private List<GameObject> KeyPrefab;
    [SerializeField] private Transform KeyBoxUI;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private int keysNeed;
    [SerializeField] private float time;

    private QTE qte;
    private List<GameObject> spawnedArrows = new List<GameObject>();
    private QTEState qteState;

    public static event Action<QTEState> OnStateChanged;
    public static event Action<QTEState> OnQTEResult;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

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

    public void StartQTE(BossState state)
    {
        if (state == BossState.Stunned)
        {
            if(qte.ListCount() > 0)
            {
                ResetStatus();
            }
            UpdateQTEState(QTEState.InProgress);
        }
        else if(state != BossState.Stunned) 
        {
            UpdateQTEState(QTEState.Ended);
            ResetStatus();
        }
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
            case QTEState.Ended:
                break;
        }
    }

    private void HandleInProgressState()
    {
        qte.GenerateRandomKey(keysNeed);
        SetArrowUI();
        UpdateQTEState(QTEState.WaitUserInput);
    }

    private void HandleSuccessState()
    {
        ResetStatus();
        OnStateChanged?.Invoke(QTEState.Success);
    }

    private void HandleFailureState()
    {
        ResetStatus();
        OnStateChanged?.Invoke(QTEState.Failure);
    }

    private void ResetStatus()
    {
        qte.ResetList();
        DestroySpawnedArrows();
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
}
