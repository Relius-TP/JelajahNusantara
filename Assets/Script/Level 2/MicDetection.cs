using UnityEngine;
using UnityEngine.UI;

public class MicDetection : MonoBehaviour
{
    [SerializeField] private Image micValueFill;

    AudioSource audioSource;
    public static float soundVolume;
    [SerializeField] private float micSensitivity;
    private bool micConnected;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        string usedDevice = PlayerPrefs.GetString("deviceName");

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
        }
    }

    /*
        Get clip data, store in sample variable
        calculate sample data to volume variable
        volume result times mic sensitivity
    */
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

        micValueFill.fillAmount = soundVolume / 10;
    }
}