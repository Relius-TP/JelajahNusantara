using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    GameStarted,
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
        UpdateGameState(GameState.GameStarted);
        Debug.Log(state);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && state != GameState.GamePaused)
        {
            UpdateGameState(GameState.GamePaused);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && state == GameState.GamePaused)
        {
            UpdateGameState(GameState.NotCaught);
        }
    }

    public void ResumeButton()
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
            case GameState.GamePaused:
                GamePauseHandler();
                break;
        }

        GameStateChanged?.Invoke(newState);
    }

    private void GamePauseHandler()
    {
        Time.timeScale = 0f;
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
        if (qteController != null && state != GameState.PlayerDie)
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

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
