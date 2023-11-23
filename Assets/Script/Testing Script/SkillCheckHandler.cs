using UnityEngine;

public class SkillCheckHandler : MonoBehaviour
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

    private bool targetMoving;
    public float result;

    private SkillCheckState state;

    private void Start()
    {
        backgroundImgWidth = backgroundImg.rect.width / 2;
        targetImgWidth = targetImg.rect.width / 2;

        UpdateSkillCheckState(SkillCheckState.ResetStat);
    }

    private void Update()
    {
        if(state == SkillCheckState.InProgress)
        {
            SkillCheckStart();
        }
    }

    private void UpdateSkillCheckState(SkillCheckState newState)
    {
        state = newState;

        switch(newState)
        {
            case SkillCheckState.ResetStat:
                ResetPosition();
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

        UpdateSkillCheckState(SkillCheckState.ResetStat);
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
            UpdateSkillCheckState(SkillCheckState.Ended);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetMoving = false;
        }

    }

    private void ResetPosition()
    {
        targetImg.localPosition = new Vector3(-backgroundImgWidth + targetImgWidth, 0, 0);
        targetMoving = true;
        UpdateSkillCheckState(SkillCheckState.InProgress);
    }
}
