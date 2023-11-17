using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionHandler : MonoBehaviour
{
    [Header("Hero List")]
    [SerializeField] private Hero[] heroes;

    [Header("UI Settings")]
    [SerializeField] private TMP_Text heroName;
    [SerializeField] private Image heroImage;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider speedBar;
    [SerializeField] private Slider visionBar;
    [SerializeField] private Slider skillCheckBar;
    [SerializeField] private Slider qteBar;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private string sceneName;

    private int index;

    private void Start()
    {
        index = 0;
        SetStatistic();
    }

    public void RightArrow()
    {
        if(index == heroes.Length - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        
        SetStatistic();
    }

    public void LeftArrow()
    {
        if (index == 0)
        {
            index = heroes.Length - 1;
        }
        else
        {
            index--;
        }
        SetStatistic();
    }

    private void SetStatistic()
    {
        heroName.text = heroes[index].heroName;
        heroImage.sprite = heroes[index].heroSprite;
        hpBar.value = heroes[index].hpPoint / 10f;
        speedBar.value = heroes[index].speed / 10f;
        visionBar.value = heroes[index].vision / 10f;
        skillCheckBar.value = heroes[index].skillCheckSpeed / 10f;
        qteBar.value = heroes[index].qteSpeed / 10f;
    }

    public void SetPlayerData()
    {
        playerData.hero_name = heroes[index].heroName;
        playerData.hero_speed = heroes[index].speed;
        playerData.hero_health = heroes[index].hpPoint;
        playerData.hero_visionRange = heroes[index].vision;

        SceneManager.LoadScene(sceneName);
    }
}
