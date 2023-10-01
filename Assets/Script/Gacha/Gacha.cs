using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gacha : MonoBehaviour
{
    public CharDatabase hero;

    [SerializeField] private Image img;
    [SerializeField] private TextMeshProUGUI nameChar;
    void Start()
    {

    }

    void Update()
    {
        img.sprite = hero.image;
        nameChar.text = hero.name;
    }
}
