using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private GameObject qteUI;
    [SerializeField] private GameObject skillCheckUI;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private GameObject typeWriterSystem;

    private void Start()
    {
        mainUI.SetActive(true);
        tutorialUI.SetActive(true);
        GameManager.GameStateChanged += UpdateCanvasUI;
    }

    private void OnDisable()
    {
        GameManager.GameStateChanged -= UpdateCanvasUI;
    }

    private void UpdateCanvasUI(GameState state)
    {
        typeWriterSystem.SetActive(state == GameState.GameStarted);
        pauseMenuUI.SetActive(state == GameState.GamePaused);
        loseUI.SetActive(state == GameState.PlayerDie);
        qteUI.SetActive(state == GameState.GetCaught);
        skillCheckUI.SetActive(state == GameState.GetItem);
    }

    public void CloseTutorialUI()
    {
        GameManager.Instance.UpdateGameState(GameState.NotCaught);
    }
}
