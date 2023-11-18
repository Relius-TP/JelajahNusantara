using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button startBtn;
    public Button optionBtn;
    public Button exitBtn;

    public GameObject option;
    public Button back;

    void Start()
    {
        startBtn.onClick.AddListener(StartGame);
        optionBtn.onClick.AddListener(Option);
        exitBtn.onClick.AddListener(QuitGame);
        back.onClick.AddListener(Back);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            option.SetActive(false);
        }
    }

    void StartGame()
    {
        StartCoroutine(LoadStartGame());
    }
    IEnumerator LoadStartGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Lobby");
    }

    void Option()
    {
        StartCoroutine(LoadOption());
    }
    IEnumerator LoadOption()
    {
        yield return new WaitForSeconds(0f);
        option.SetActive(true);
    }
    void Back()
    {
        option.SetActive(false);
    }

    void QuitGame()
    {
        StartCoroutine(LoadQuitGame());
    }
    IEnumerator LoadQuitGame()
    {
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
