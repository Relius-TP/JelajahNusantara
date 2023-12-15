using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    private float shakerTimer;

    private void OnEnable()
    {
        Health.OnTakeDamage += ShakeCamera;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= ShakeCamera;
    }

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakerTimer = time;
    }

    private void Update()
    {
        if(shakerTimer > 0)
        {
            shakerTimer -= Time.deltaTime;
            if (shakerTimer <= 0f)
            {
                //Times up, intensity to zero
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}
