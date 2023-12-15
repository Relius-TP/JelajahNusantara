using System;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private Transform healthIconBox;
    [SerializeField] private PlayerData playerData;

    private List<GameObject> healthList;

    public static event Action<float, float> OnTakeDamage;

    private void OnEnable()
    {
        PotionDetection.GetHealthPotion += AddHealth;
        QTEController.OnQTEResult += TakeDamage;
    }

    private void OnDestroy()
    {
        PotionDetection.GetHealthPotion -= AddHealth;
        QTEController.OnQTEResult -= TakeDamage;
    }

    private void Awake()
    {
        healthList = new List<GameObject>();
    }

    private void Start()
    {
        SetHealthUI();
    }

    private void AddHealth(int value)
    {
        if(health != playerData.hero_health)
        {
            health += value;
        }

        SetHealthUI();
    }

    private void TakeDamage(QTEState state)
    {
        if(state == QTEState.Failure)
        {
            if (health > 0)
            {
                health--;
            }
            
            if(health <= 0)
            {
                Die();
            }

            SetHealthUI();
            OnTakeDamage?.Invoke(5f, .1f);
        }
    }

    private void Die()
    {
        GameManager.Instance.UpdateGameState(GameState.PlayerDie);
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
