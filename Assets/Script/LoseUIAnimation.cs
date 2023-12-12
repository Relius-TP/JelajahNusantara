using System.Collections;
using UnityEngine;

public class LoseUIAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform loseText;
    [SerializeField] private GameObject restart_btn;
    [SerializeField] private GameObject mainMenu_btn;

    private void OnEnable()
    {
        StartCoroutine(StartAnimation());
    }

    private void Awake()
    {
        restart_btn.SetActive(false);
        mainMenu_btn.SetActive(false);
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(2);
        restart_btn.SetActive(true);
        yield return new WaitForSeconds(1);
        mainMenu_btn.SetActive(true);
    }
}
