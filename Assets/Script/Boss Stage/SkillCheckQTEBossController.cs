using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public float result;

    [Header("QTE Settings")]
    [SerializeField] private TMP_Text QTE_TextUI;
    [SerializeField] private int keysNeed;

    private List<KeyCode> keys;
    private List<KeyCode> inputFromUser;

    private bool gettingKey = false;
    private bool needGenerateKey = true;
    private bool isFailed = false;
    private int counter;

    public static bool bossStun = true;
    public static event Action<float> GiveDamage;


    private enum State
    {
        WaitingResetSkillCheck,
        WaitingInputPlayer,
        WaitingCheckResult
    }

    private State qteState = State.WaitingInputPlayer;
    private State skillCheckState = State.WaitingResetSkillCheck;

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
            if(qteState == State.WaitingInputPlayer) 
            {
                QTEStart();
            }
            if(skillCheckState == State.WaitingResetSkillCheck)
            {
                ResetPosition();
                SetRandomPosition();
                skillCheckState = State.WaitingInputPlayer;
            }
            else if(skillCheckState == State.WaitingInputPlayer)
            {
                SkillCheckStart();
            }

            if(qteState == State.WaitingCheckResult && skillCheckState == State.WaitingCheckResult)
            {
                Boss.isTakingDamage = true;
                CheckResult();
            }
        }
        else
        {
            needGenerateKey = true;
            qteState = State.WaitingInputPlayer;
            skillCheckState = State.WaitingResetSkillCheck;
            result = 0;
        }
    }


    private void CheckResult()
    {
        qteState = State.WaitingInputPlayer;
        skillCheckState = State.WaitingResetSkillCheck;

        if (!isFailed && result != 0)
        {
            if (result == 1)
            {
                GiveDamage?.Invoke(10);
            }
            else if (result == 2)
            {
                GiveDamage?.Invoke(20);
            }
            else if (result == 3)
            {
                GiveDamage?.Invoke(35);
            }
        }

        needGenerateKey = true;
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
                result = 1;
            }
            else if (targetImg.position.x >= minNormalRange.position.x && targetImg.position.x <= maxNormalRange.position.x
                && !(targetImg.position.x > minStrongRange.position.x && targetImg.position.x < maxStrongRange.position.x))
            {
                result = 2;
            }
            else if (targetImg.position.x >= minStrongRange.position.x && targetImg.position.x <= maxStrongRange.position.x)
            {
                result = 3;
            }
            else
            {
                result = 0; 
            }

            skillCheckState = State.WaitingCheckResult;
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
        float randomPosition = UnityEngine.Random.Range(-backgroundImgWidth + goalOffset, backgroundImgWidth - goalOffset);
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
                if (Input.GetKeyDown(KeyCode.UpArrow))
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

    private void GenerateKeys()
    {
        needGenerateKey = false;

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
            else if (randomNumber == 3)
            {
                keys.Add(KeyCode.RightArrow);
            }
        }
        SetText(keys);
        gettingKey = true;
        isFailed = false;
        counter = 0;
        inputFromUser.Clear();
    }

    private void SetText(List<KeyCode> keys)
    {
        string keysText = "";

        foreach (KeyCode key in keys)
        {
            if (key == KeyCode.UpArrow)
            {
                keysText += "[Up]";
            }
            else if (key == KeyCode.DownArrow)
            {
                keysText += "[Down]";
            }
            else if (key == KeyCode.LeftArrow)
            {
                keysText += "[Left]";
            }
            else if(key == KeyCode.RightArrow)
            {
                keysText += "[Right]";
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
            qteState = State.WaitingCheckResult;
        }
        else
        {
            QTE_TextUI.SetText("Failed!!!!");
            qteState = State.WaitingCheckResult;
        }
    }
}
