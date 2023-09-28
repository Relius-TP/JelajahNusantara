using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;

public class QteSystem : MonoBehaviour
{
    [SerializeField] private float duration = 2.0f;
    [SerializeField] private int maxButton = 3;
    [SerializeField] private TMP_Text textUI;

    public void GetQteButton()
    {
        textUI.SetText("");

        ArrayList randomNumber = new();

        for(int i = 0; i < maxButton; i++)
        {
            randomNumber.Add(Random.Range(1, 5));
        }

        SetQteText(randomNumber);
        StartCoroutine(QteTimer());
    }

    private void SetQteText(ArrayList randomNumber)
    {
        for(int i = 0;i < maxButton; i++)
        {
            if((int) randomNumber[i] == 1)
            {
                textUI.SetText(textUI.text + "[UP]");
            }
            else if ((int)randomNumber[i] == 2)
            {
                textUI.SetText(textUI.text + "[DOWN]");
            }
            else if ((int)randomNumber[i] == 3)
            {
                textUI.SetText(textUI.text + "[LEFT]");
            }
            else if ((int)randomNumber[i] == 4)
            {
                textUI.SetText(textUI.text + "[RIGHT]");
            }
        }
    }

    IEnumerator QteTimer()
    {
        yield return new WaitForSeconds(duration);
        textUI.SetText("Failed");
    }
}
