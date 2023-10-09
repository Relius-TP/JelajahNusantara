using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine.UIElements;

public class QteSystem : MonoBehaviour
{
    public TMP_Text DisplayText;
    public TMP_Text PassBox;
    public int QTEGen;
    public int WaitingForKey;
    public int CorrectKey;
    public int CountingDown;

    private void Update()
    {
        if(WaitingForKey == 0)
        {
            QTEGen = Random.Range(1, 4);
            CountingDown = 1;
            StartCoroutine(CountDown());

            if(QTEGen == 1)
            {
                WaitingForKey = 1;
                DisplayText.SetText("[E]");
            }
            if(QTEGen == 2)
            {
                WaitingForKey = 1;
                DisplayText.SetText("[T]");
            }
            if (QTEGen == 3)
            {
                WaitingForKey = 1;
                DisplayText.SetText("[R]");
            }
        }

        if(QTEGen == 1)
        {
            if (Input.anyKeyDown)
            {
                CheckButton(KeyCode.E);
            }
        }
        if (QTEGen == 2)
        {
            if (Input.anyKeyDown)
            {
                CheckButton(KeyCode.T);
            }
        }
        if (QTEGen == 3)
        {
            if (Input.anyKeyDown)
            {
                CheckButton(KeyCode.R);
            }
        }
    }

    private void CheckButton(KeyCode input)
    {
        if (Input.GetKeyDown(input))
        {
            CorrectKey = 1;
            StartCoroutine(KeyPressing());
        }
        else
        {
            CorrectKey = 2;
            StartCoroutine(KeyPressing());
        }
    }

    private void ResetStatus()
    {
        CorrectKey = 0;
        PassBox.SetText("");
        DisplayText.SetText("");
        WaitingForKey = 0;
        CountingDown = 1;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    IEnumerator KeyPressing()
    {
        QTEGen = 4;
        if(CorrectKey == 1)
        {
            CountingDown = 2;
            PassBox.SetText("Pass!");
            yield return new WaitForSecondsRealtime(1.5f);
            ResetStatus();
        }
        if (CorrectKey == 2)
        {
            CountingDown = 2;
            PassBox.SetText("Fail!!!!");
            yield return new WaitForSecondsRealtime(1.5f);
            ResetStatus();
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        if(CountingDown == 1)
        {
            QTEGen = 4;
            CountingDown = 2;
            PassBox.SetText("Fail!!!!");
            yield return new WaitForSecondsRealtime(1.5f);
            ResetStatus();
        }
    }
}
