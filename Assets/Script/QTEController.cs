using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class QTEController : MonoBehaviour
{
    public TMP_Text QTE_TextUI;
    public GameObject QTE_Box;
    public int keysNeed;

    private List<KeyCode> keys;
    private List<KeyCode> inputFromUser;
    [SerializeField] private List<GameObject> arrowPrefab;

    public static bool gettingKey = false;
    public static bool generateKey = false;
    private bool isFailed = false;

    public static Action<bool> OnQTEResult;

    private void OnEnable()
    {
        if(generateKey)
        {
            GenerateKeys();
        }
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
            if (Input.anyKeyDown)
            {
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    inputFromUser.Add(KeyCode.UpArrow);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    inputFromUser.Add(KeyCode.DownArrow);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    inputFromUser.Add(KeyCode.LeftArrow);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    inputFromUser.Add(KeyCode.RightArrow);
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
            int randomNumber = UnityEngine.Random.Range(0, 4);

            if (randomNumber == 0)
            {
                keys.Add(KeyCode.UpArrow);
            }
            else if (randomNumber == 1)
            {
                keys.Add(KeyCode.DownArrow);
            }
            else if (randomNumber == 2)
            {
                keys.Add(KeyCode.LeftArrow);
            }
            else if(randomNumber == 3)
            {
                keys.Add(KeyCode.RightArrow);
            }
        }
        SetText(keys);
        gettingKey = true;
        generateKey = false;
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
        foreach (KeyCode key in keys)
        {
            if (key == KeyCode.UpArrow)
            {
                var arrow = Instantiate(arrowPrefab[3]) as GameObject;
                arrow.transform.SetParent(QTE_Box.transform, false);
            }
            else if (key == KeyCode.DownArrow)
            {
                var arrow = Instantiate(arrowPrefab[0]) as GameObject;
                arrow.transform.SetParent(QTE_Box.transform, false);
            }
            else if (key == KeyCode.LeftArrow)
            {
                var arrow = Instantiate(arrowPrefab[1]) as GameObject;
                arrow.transform.SetParent(QTE_Box.transform, false);
            }
            else if(key == KeyCode.RightArrow)
            {
                var arrow = Instantiate(arrowPrefab[2]) as GameObject;
                arrow.transform.SetParent(QTE_Box.transform, false);
            }
        }
    }
}
