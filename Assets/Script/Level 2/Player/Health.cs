using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private Transform healthIconBox;
    [SerializeField] private PlayerData playerData;

    private List<GameObject> healthList;

    private void Awake()
    {
        healthList = new List<GameObject>();
    }

    private void Start()
    {
        SetHealthUI();
    }

    private void AddHealth()
    {
        if(health != playerData.hero_health)
        {
            health++;
        }
    }

    private void TakeDamage()
    {
        if(health > 0)
        {
            health--;
        }
    }

    private void SetHealthUI()
    {
        DestroyHealthUI();

        for(int i = 0; i < health; i++)
        {
            var icon = Instantiate(healthPrefab, healthIconBox);
            healthList.Add(icon);
        }
    }

    private void DestroyHealthUI()
    {
        foreach(var icon in healthList)
        {
            Destroy(icon);
        }

        healthList.Clear();
    }
}
