using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject QteUi;
    public GameObject QteSys;

    private void Start()
    {
        QteSys.SetActive(false);
        QteUi.SetActive(false);
    }

    private void OnEnable()
    {
        Player.OnCaught += GetCaught;
    }

    private void OnDisable()
    {
        Player.OnCaught -= GetCaught;
    }

    private void GetCaught()
    {
        Time.timeScale = 0f;
        QteUi.SetActive(true);
        QteSys.SetActive(true);
    }
}
