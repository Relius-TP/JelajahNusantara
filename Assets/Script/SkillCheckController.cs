using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheckController : MonoBehaviour
{
    public RectTransform goalTarget;
    public RectTransform targetCursor;
    public RectTransform background;
    public RectTransform minXRangeTarget;
    public RectTransform maxXRangeTarget;

    private float backgroundWidth;
    private float targetCursorWidth;
    private float goalTargetWidth;
    private float randomPosition;
    
    private Vector3 targetCursorStartPos;

    public float cursorSpeed = 1.5f;

    private bool isMoving = true;
    private bool isRunningResults = false;

    public static bool isWaitingInput = false;
    public static Action<bool> OnSkillCheckResults;

    private void Start()
    {
        ResetCursorPosition();
        RandomGoalPosition();
        goalTargetWidth = goalTarget.rect.width / 2;
    }

    void Update()
    {
        if (isWaitingInput && !isRunningResults)
        {
            if (isMoving)
            {
                if (targetCursor.localPosition.x + targetCursorWidth < background.localPosition.x + backgroundWidth)
                {
                    targetCursor.transform.Translate(Vector2.right * Time.deltaTime * cursorSpeed);
                }
                else
                {
                    isRunningResults = true;
                    Debug.Log("Gagal");
                    StartCoroutine(Results(false));
                }
            }
            else
            {
                if (targetCursor.position.x >= minXRangeTarget.position.x
                    && targetCursor.position.x <= maxXRangeTarget.position.x)
                {
                    Debug.Log("Berhasil");
                    isRunningResults = true;
                    StartCoroutine(Results(true));
                }
                else
                {
                    isRunningResults = true;
                    Debug.Log("Gagal");
                    StartCoroutine(Results(false));
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isMoving = false;
            }
        }
    }


    private void ResetCursorPosition()
    {
        backgroundWidth = background.rect.width / 2;
        targetCursorWidth = targetCursor.rect.width / 2;
        targetCursorStartPos = new Vector3(targetCursorWidth - backgroundWidth, background.localPosition.y, 0);
        targetCursor.localPosition = targetCursorStartPos;
        isMoving = true;
        isWaitingInput = false;
        isRunningResults = false;
    }

    private void RandomGoalPosition()
    {
        float goalOffset = backgroundWidth / 2;
        randomPosition = UnityEngine.Random.Range(-backgroundWidth + goalOffset, backgroundWidth - goalOffset);
        goalTarget.localPosition = new Vector3(randomPosition + goalTargetWidth, background.localPosition.y);
        minXRangeTarget.localPosition = new Vector3(-goalTargetWidth, 0, 0);
        maxXRangeTarget.localPosition = new Vector3(goalTargetWidth, 0, 0);
    }

    IEnumerator Results(bool result)
    {
        yield return new WaitForSeconds(1.5f);
        ResetCursorPosition();
        RandomGoalPosition();
        OnSkillCheckResults?.Invoke(result);
        yield break;
    }
}
