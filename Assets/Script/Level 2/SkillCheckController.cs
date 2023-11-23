using System;
using UnityEngine;

public class SkillCheckController : MonoBehaviour
{
    [Header("Skill Check Settings")]
    [SerializeField] private RectTransform goalTarget;
    [SerializeField] private RectTransform targetCursor;
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform minXRangeTarget;
    [SerializeField] private RectTransform maxXRangeTarget;
    [SerializeField] private float cursorSpeed = 1.5f;

    private float backgroundWidth;
    private float targetCursorWidth;
    private float goalTargetWidth;
    private float randomPosition;

    private Vector3 targetCursorStartPos;

    private bool targetMoving = true;

    private SkillCheckState state;
    
    public static Action<SkillCheckState> OnSkillCheckResults;

    private void Awake()
    {
        goalTargetWidth = goalTarget.rect.width / 2;
    }

    private void Start()
    {
        UpdateSkillCheckState(SkillCheckState.Ended);
    }

    void Update()
    {
        if (state == SkillCheckState.InProgress)
        {
            SkillCheckStart();
        }
    }

    private void SkillCheckStart()
    {
        if (targetMoving)
        {
            if (targetCursor.localPosition.x + targetCursorWidth < background.localPosition.x + backgroundWidth)
            {
                targetCursor.transform.Translate(cursorSpeed * Time.unscaledDeltaTime * Vector2.right);
            }
            else
            {
                targetMoving = false;
            }
        }
        else
        {
            UpdateSkillCheckState(SkillCheckState.Ended);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetMoving = false;
        }
    }

    public void StartSkillCheck()
    {
        UpdateSkillCheckState(SkillCheckState.ResetStat);
    }

    private void UpdateSkillCheckState(SkillCheckState newState)
    {
        state = newState;

        switch (newState)
        {
            case SkillCheckState.ResetStat:
                ResetPosition();
                RandomGoalPosition();
                UpdateSkillCheckState(SkillCheckState.InProgress);
                break;
            case SkillCheckState.InProgress:
                break;
            case SkillCheckState.Ended:
                CheckResult();
                break;
        }
    }

    private void CheckResult()
    {
        if (targetCursor.position.x >= minXRangeTarget.position.x
                    && targetCursor.position.x <= maxXRangeTarget.position.x)
        {
            OnSkillCheckResults?.Invoke(SkillCheckState.Victory);
        }
        else
        {
            OnSkillCheckResults?.Invoke(SkillCheckState.Failure);
        }

        GameManager.Instance.UpdateGameState(GameState.NotCaught);
    }

    private void ResetPosition()
    {
        backgroundWidth = background.rect.width / 2;
        targetCursorWidth = targetCursor.rect.width / 2;
        targetCursorStartPos = new Vector3(targetCursorWidth - backgroundWidth, background.localPosition.y, 0);
        targetCursor.localPosition = targetCursorStartPos;
        targetMoving = true;
    }

    private void RandomGoalPosition()
    {
        float goalOffset = backgroundWidth / 2;
        randomPosition = UnityEngine.Random.Range(-backgroundWidth + goalOffset + goalOffset / 2, backgroundWidth - goalOffset);
        goalTarget.localPosition = new Vector3(randomPosition + goalTargetWidth, background.localPosition.y);
        minXRangeTarget.localPosition = new Vector3(-goalTargetWidth, 0, 0);
        maxXRangeTarget.localPosition = new Vector3(goalTargetWidth, 0, 0);
    }
}
