using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightAnimation : MonoBehaviour
{
    private Light2D playerLight;

    private bool lightIncrease = true;
    private float currentIntensity;

    [SerializeField] private float heartBeatLightSpeed;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float minIntensity;

    private void Awake()
    {
        playerLight = GetComponent<Light2D>();
    }

    private void Start()
    {
        currentIntensity = playerLight.intensity;
    }

    private void Update()
    {
        if (lightIncrease)
        {
            currentIntensity = Mathf.Lerp(currentIntensity, maxIntensity, Time.deltaTime * heartBeatLightSpeed);
            if(currentIntensity >= maxIntensity - 0.1f)
            {
                lightIncrease = false;
            }
        }
        else
        {
            currentIntensity = Mathf.Lerp(currentIntensity, minIntensity, Time.deltaTime * heartBeatLightSpeed);
            if (currentIntensity <= minIntensity + 0.1f)
            {
                lightIncrease = true;
            }
        }

        playerLight.intensity = currentIntensity;
    }
}
