using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject option;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            option.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Lobby");
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
