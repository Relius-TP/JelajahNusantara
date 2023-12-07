using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private float firstVisionRange;
    private float currentVisionRange;
    private float visionPotionEffect;
    private float totalVisionRange;
    private float soundValue;

    private Light2D playerVision;

    public static event Action<float> OnVisionRangeChange;

    private void OnEnable()
    {
        MicDetection.OnSoundValueChanged += SetSoundValue;
        PotionDetection.GetVisionPotion += OnVisionPotionEffect;
    }

    private void OnDestroy()
    {
        MicDetection.OnSoundValueChanged -= SetSoundValue;
        PotionDetection.GetVisionPotion -= OnVisionPotionEffect;
    }

    private void Awake()
    {
        firstVisionRange = playerData.hero_visionRange;
        playerVision = GetComponentInChildren<Light2D>();
    }

    private void Update()
    {
        totalVisionRange = firstVisionRange + visionPotionEffect;

        if (soundValue >= 0.5f)
        {
            currentVisionRange = Mathf.Lerp(currentVisionRange, totalVisionRange + 3f, 0.1f);
        }
        else if (soundValue < 1)
        {
            currentVisionRange = Mathf.Lerp(currentVisionRange, totalVisionRange, 0.1f);
        }

        playerVision.pointLightOuterRadius = currentVisionRange;
        OnVisionRangeChange?.Invoke(currentVisionRange);
    }

    private void SetSoundValue(float newSoundValue)
    {
        soundValue = newSoundValue;
    }

    private void OnVisionPotionEffect(float value, float duration)
    {
        visionPotionEffect = value;
        StartCoroutine(PotionEffectDuration(duration));
    }

    IEnumerator PotionEffectDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        visionPotionEffect = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, currentVisionRange);
    }
}
