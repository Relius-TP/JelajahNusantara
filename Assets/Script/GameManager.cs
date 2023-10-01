using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject QteUi;
    QteSystem qteSystem;

    private void Start()
    {
        QteUi.SetActive(false);
        qteSystem = GameObject.FindObjectOfType<QteSystem>().GetComponent<QteSystem>();
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
        QteUi.SetActive(true);
        qteSystem.GetQteButton();
    }
}
