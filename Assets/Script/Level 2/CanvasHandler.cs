using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private GameObject qteUI;
    [SerializeField] private GameObject skillCheckUI;

    private void Start()
    {
        mainUI.SetActive(true);
        GameManager.GameStateChanged += UpdateCanvasUI;
    }

    private void OnDestroy()
    {
        GameManager.GameStateChanged -= UpdateCanvasUI;
    }

    private void UpdateCanvasUI(GameState state)
    {
        //pauseMenuUI.SetActive(state == GameState.GamePaused);
        loseUI.SetActive(state == GameState.PlayerDie);
        qteUI.SetActive(state == GameState.GetCaught);
        skillCheckUI.SetActive(state == GameState.GetItem);
    }
}
