using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject[] healthICon;
    public int healthPoin = 3;

    private void OnEnable()
    {
        QteSystem.OnQTEResult += SetHealth;
    }

    private void SetHealth(bool result)
    {
        if(!result)
        {
            healthPoin--;
        }
    }

    private void Update()
    {
        ResetHealthUI();
        for (int i = 0; i < healthPoin; i++)
        {
            healthICon[i].SetActive(true);
        }
    }

    private void ResetHealthUI()
    {
        foreach (var item in healthICon)
        {
            item.SetActive(false);
        }
    }
}
