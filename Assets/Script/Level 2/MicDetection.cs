using System;
using UnityEngine;
using UnityEngine.UI;

public class MicDetection : MonoBehaviour
{
    [SerializeField] private Image micValueFill;
    [SerializeField] private float micSensitivity;
    [SerializeField] private AudioSource audioSource;

    public float soundVolume;
    private bool micConnected;

    public static event Action<float> OnSoundValueChanged;

#if UNITY_WEBGL && !UNITY_EDITOR
        void Awake()
        {
            Microphone.Init();
            Microphone.QueryAudioInput();
        }
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
        void Update()
        {
            Microphone.Update();
            SetMicValueUI();
            GetVolumeFromMicrophone();
        }
#endif


#if !UNITY_WEBGL
    private void Start()
    {

        if(Microphone.devices.Length >= 0)
        {
            micConnected = true;
            audioSource.clip = Microphone.Start(null, true, 1, 44100);
            while (!(Microphone.GetPosition(null) > 0)) { }
            audioSource.Play();
        }
        else
        {
            micConnected = false;
            Debug.Log("No microphone device detected");
        }
    }

    private void Update()
    {
        if(micConnected)
        {
            GetVolumeFromMicrophone();
            SetMicValueUI();
        }
    }
#endif

    private void SetMicValueUI()
    {
        micValueFill.fillAmount = soundVolume;
    }

    private void GetVolumeFromMicrophone()
    {
#if !UNITY_WEBGL
        float[] samples = new float[audioSource.clip.samples];
        float volume = 0.0f;

        audioSource.clip.GetData(samples, 0);

        for (int i = 0; i < samples.Length; i++)
        {
            volume += Mathf.Abs(samples[i]);
        }

        volume /= samples.Length;
        soundVolume = volume * micSensitivity;
#endif
#if UNITY_WEBGL && !UNITY_EDITOR
        soundVolume = Microphone.volumes[0] * micSensitivity;
#endif

        OnSoundValueChanged?.Invoke(soundVolume);
    }
}