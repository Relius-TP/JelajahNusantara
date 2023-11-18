using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillCheckQTEBossController : MonoBehaviour
{
    [Header("Skill Check Settings")]
    [SerializeField] private RectTransform backgroundImg;
    [SerializeField] private RectTransform targetImg;
    [SerializeField] private RectTransform weakGoalImg;
    [SerializeField] private RectTransform normalGoalImg;
    [SerializeField] private RectTransform strongGoalImg;
    [SerializeField] private float targetSpeed;

    [Header("Skill Check Goal Range")]
    [SerializeField] private RectTransform minWeakRange;
    [SerializeField] private RectTransform maxWeakRange;
    [SerializeField] private RectTransform minNormalRange;
    [SerializeField] private RectTransform maxNormalRange;
    [SerializeField] private RectTransform minStrongRange;
    [SerializeField] private RectTransform maxStrongRange;

    private float backgroundImgWidth;
    private float targetImgWidth;
    private float weakGoalImgWidth;
    private float normalGoalImgWidth;
    private float strongGoalImgWidth;

    private bool targetMoving = true;

    [Header("QTE Settings")]
    [SerializeField] private TMP_Text QTE_TextUI;
    [SerializeField] private int keysNeed;

    private List<KeyCode> keys;
    private List<KeyCode> inputFromUser;

    private bool gettingKey = false;
    private bool needGenerateKey = true;
    private bool isFailed = false;

    public static bool bossStun = true;

    void Awake()
    {
        backgroundImgWidth = backgroundImg.rect.width / 2;
        targetImgWidth = targetImg.rect.width / 2;
        weakGoalImgWidth = weakGoalImg.rect.width / 2;
        normalGoalImgWidth = normalGoalImg.rect.width / 2;
        strongGoalImgWidth = strongGoalImg.rect.width / 2;

        inputFromUser = new List<KeyCode>();
        QTE_TextUI.SetText("[Button][Here]");
    }

    // Update is called once per frame
    void Update()
    {
        if (bossStun)
        {
            SkillCheckStart();
            QTEStart();
        }
    }

    private void SkillCheckStart()
    {
        if (targetMoving)
        {
            if (targetImg.localPosition.x + targetImgWidth < backgroundImgWidth)
            {
                targetImg.transform.Translate(Vector3.right * Time.deltaTime * targetSpeed);
            }
            else
            {
                targetMoving = false;
            }
        }
        else
        {
            if (targetImg.position.x >= minWeakRange.position.x & targetImg.position.x <= maxWeakRange.position.x
                        && !(targetImg.position.x > minNormalRange.position.x && targetImg.position.x < maxNormalRange.position.x)
                        && !(targetImg.position.x > minStrongRange.position.x && targetImg.position.x < maxStrongRange.position.x))
            {
                Debug.Log("Weak Spot");
                ResetPosition();
                SetRandomPosition();
            }
            else if (targetImg.position.x >= minNormalRange.position.x && targetImg.position.x <= maxNormalRange.position.x
                && !(targetImg.position.x > minStrongRange.position.x && targetImg.position.x < maxStrongRange.position.x))
            {
                Debug.Log("Normal Spot");
                ResetPosition();
                SetRandomPosition();
            }
            else if (targetImg.position.x >= minStrongRange.position.x && targetImg.position.x <= maxStrongRange.position.x)
            {
                Debug.Log("Strong Spot");
                ResetPosition();
                SetRandomPosition();
            }
            else
            {
                Debug.Log("Failedd");
                ResetPosition();
                SetRandomPosition();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetMoving = false;
        }
    }

    private void ResetPosition()
    {
        targetImg.localPosition = new Vector3(-backgroundImgWidth, 0, 0);
        targetMoving = true;
    }

    private void SetRandomPosition()
    {
        float goalOffset = backgroundImgWidth / 2;
        float randomPosition = Random.Range(-backgroundImgWidth + goalOffset, backgroundImgWidth - goalOffset);
        weakGoalImg.localPosition = new Vector3(randomPosition + weakGoalImgWidth, backgroundImg.localPosition.y, 0);
        minWeakRange.localPosition = new Vector3(-weakGoalImgWidth, 0, 0);
        maxWeakRange.localPosition = new Vector3(weakGoalImgWidth, 0, 0);
        minNormalRange.localPosition = new Vector3(-normalGoalImgWidth, 0, 0);
        maxNormalRange.localPosition = new Vector3(normalGoalImgWidth, 0, 0); ;
        minStrongRange.localPosition = new Vector3(-strongGoalImgWidth, 0, 0);
        maxStrongRange.localPosition = new Vector3(strongGoalImgWidth, 0, 0);
    }

    private void QTEStart()
    {
        if(needGenerateKey)
        {
            GenerateKeys();
        }

        if (gettingKey)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    inputFromUser.Add(KeyCode.E);
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    inputFromUser.Add(KeyCode.R);
                }
                else if (Input.GetKeyDown(KeyCode.T))
                {
                    inputFromUser.Add(KeyCode.T);
                }
            }

            if (inputFromUser.Count >= keys.Count)
            {
                CheckKeys(keys);
            }
        }
    }

    private void GenerateKeys()
    {
        needGenerateKey = false;

        keys = new List<KeyCode>();

        for (int i = 0; i < keysNeed; i++)
        {
            int randomNumber = Random.Range(0, 3);

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
        }
        else
        {
            QTE_TextUI.SetText("Failed!!!!");
        }

        needGenerateKey = true;
    }
}
