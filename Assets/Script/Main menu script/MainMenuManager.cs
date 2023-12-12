using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject option;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            option.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Option()
    {
        option.SetActive(true);
    }

    public void Back()
    {
        option.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
