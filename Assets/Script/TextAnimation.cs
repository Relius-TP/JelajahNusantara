using System;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    private TMP_Text text;
    private Animator animator;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        BossStageGameManager.OnSuccess += CheckDamage;
    }

    private void OnDisable()
    {
        BossStageGameManager.OnSuccess -= CheckDamage;
    }

    private void CheckDamage(float damage)
    {
        if(damage == 35)
        {
            text.text = "PERFECT";
            text.color = Color.green;
        }
        else if(damage == 20)
        {
            text.text = "GREAT";
            text.color = Color.yellow;
        }
        else if(damage == 10) 
        {
            text.text = "GOOD";
            text.color = Color.blue;
        }
        else if(damage == 0)
        {
            text.text = "BAD";
            text.color = Color.red;
        }

        animator.SetTrigger("PopUp");
    }
}
