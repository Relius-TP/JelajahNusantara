using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QTEController : MonoBehaviour
{
    public TMP_Text QTE_TextUI;
    public int keysNeed;

    private List<KeyCode> keys;
    private List<KeyCode> inputFromUser;

    public static bool gettingKey = false;
    private bool isFailed = false;

    public static Action<bool> OnQTEResult;

    private void OnEnable()
    {
        GenerateKeys();
    }

    private void Awake()
    {
        inputFromUser = new List<KeyCode>();
        QTE_TextUI.SetText("[Button][Here]");
    }

    private void Update()
    {
        if (gettingKey)
        {
            StartCoroutine(RemainingTime());
            if (Input.anyKeyDown) // Check user input
            {
                foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(key))
                    {
                        inputFromUser.Add(key);
                        break; //Stop loop when same key found
                    }
                }
            }

            if (inputFromUser.Count >= keys.Count)
            {
                CheckKeys(keys);
            }
        }
    }

    //Update text when time runs out
    IEnumerator RemainingTime()
    {
        yield return new WaitForSecondsRealtime(2f);
        if(gettingKey)
        {
            gettingKey = false;
            QTE_TextUI.SetText("Failed!!!!");
            OnQTEResult?.Invoke(false);
            Time.timeScale = 1;
        }
        gameObject.SetActive(false);
        QTE_TextUI.SetText("");
    }

    private void GenerateKeys()
    {
        keys = new List<KeyCode>();

        for (int i = 0; i < keysNeed; i++)
        {
            int randomNumber = UnityEngine.Random.Range(0, 3);

            if (randomNumber == 0)
            {
                keys.Add(KeyCode.E);
            }
            else if (randomNumber == 1)
            {
                keys.Add(KeyCode.R);
            }
            else if (randomNumber == 2)
            {
                keys.Add(KeyCode.T);
            }
        }
        SetText(keys);
        gettingKey = true;
        isFailed = false;
        inputFromUser.Clear();
    }

    private void CheckKeys(List<KeyCode> keys)
    {
        gettingKey = false;

        for (int i = 0; i < keys.Count; i++)
        {
            if (inputFromUser[i] != keys[i])
            {
                isFailed = true;
                break;
            }
        }

        if (!isFailed)
        {
            QTE_TextUI.SetText("Success");
            OnQTEResult?.Invoke(true);
            Time.timeScale = 1;
        }
        else
        {
            QTE_TextUI.SetText("Failed!!!!");
            OnQTEResult?.Invoke(false);
            Time.timeScale = 1;
        }
    }

    private void SetText(List<KeyCode> keys)
    {
        string keysText = "";

        foreach (KeyCode key in keys)
        {
            if (key == KeyCode.E)
            {
                keysText += "[E]";
            }
            else if (key == KeyCode.R)
            {
                keysText += "[R]";
            }
            else if (key == KeyCode.T)
            {
                keysText += "[T]";
            }
        }

        QTE_TextUI.SetText(keysText);
    }
}
