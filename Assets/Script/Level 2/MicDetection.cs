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

    private void SetMicValueUI()
    {
        micValueFill.fillAmount = soundVolume;
    }

    private void GetVolumeFromMicrophone()
    {
        float[] samples = new float[audioSource.clip.samples];
        float volume = 0.0f;

        audioSource.clip.GetData(samples, 0);

        for (int i = 0; i < samples.Length; i++)
        {
            volume += Mathf.Abs(samples[i]);
        }

        volume /= samples.Length;
        soundVolume = volume * micSensitivity;

        OnSoundValueChanged?.Invoke(soundVolume);
    }
}