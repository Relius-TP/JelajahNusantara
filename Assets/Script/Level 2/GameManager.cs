using System;
using UnityEngine;

public enum GameState
{
    GamePaused,
    NotCaught,
    GetCaught,
    GetItem,
    PlayerDie,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameState state;
    private QTEController qteController;
    private SkillCheckController skillCheckController;

    public static event Action<GameState> GameStateChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        qteController = FindFirstObjectByType<QTEController>();
        skillCheckController = FindFirstObjectByType<SkillCheckController>();
    }

    private void Start()
    {
        UpdateGameState(GameState.NotCaught);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.NotCaught:
                NotCaughtHandler();
                break;
            case GameState.GetCaught:
                GetCaughtGetItemHandler();
                break;
            case GameState.GetItem:
                GetCaughtGetItemHandler();
                break;
            case GameState.PlayerDie:
                break;
        }

        GameStateChanged?.Invoke(newState);
    }

    private void NotCaughtHandler()
    {
        Time.timeScale = 1f;
    }

    private void GetCaughtGetItemHandler()
    {
        Time.timeScale = 0f;
    }

    public void PlayerGotCaught()
    {
        if (qteController != null)
        {
            UpdateGameState(GameState.GetCaught);
            qteController.StartQTEForGetCaught();
        }
    }

    public void PlayerGetItem()
    {
        if(skillCheckController != null)
        {
            UpdateGameState(GameState.GetItem);
            skillCheckController.StartSkillCheck();
        }
    }
}
