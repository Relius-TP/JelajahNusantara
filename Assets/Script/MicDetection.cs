using UnityEngine;

public class MicDetection : MonoBehaviour
{
    public float soundValue; // Ini adalah variabel untuk menyimpan nilai suara dari mikrofon.
    public string microphoneDeviceName = null; // Nama perangkat mikrofon (kosongkan untuk menggunakan mikrofon default).
    public int recordingDuration = 1; // Durasi rekaman dalam detik.

    private AudioSource audioSource;

    public enum MicID
    {
        Mic1 = 0,
        Mic2 = 1,
        Mic3 = 2
    }

    public MicID selectedMicrophone = MicID.Mic1; // Set this in the Inspector

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (Microphone.devices.Length == 0)
        {
            Debug.LogError("No microphone detected.");
            return;
        }

        int micIndex = (int)selectedMicrophone;

        // Check if the selected microphone index is within the range of available devices.
        if (micIndex >= 0 && micIndex < Microphone.devices.Length)
        {
            microphoneDeviceName = Microphone.devices[micIndex];

            // Mulai merekam dari mikrofon.
            audioSource.clip = Microphone.Start(microphoneDeviceName, true, recordingDuration, AudioSettings.outputSampleRate);

            // Tunggu hingga rekaman dimulai.
            while (Microphone.GetPosition(microphoneDeviceName) <= 0) { }

            // Putar rekaman audio.
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Selected microphone index is out of range.");
        }
    }

    void Update()
    {
        // Ambil nilai suara dari audio yang sedang diputar dan simpan ke soundValue.
        float[] samples = new float[audioSource.clip.samples];
        audioSource.clip.GetData(samples, 0);

        float sum = 0f;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }

        soundValue = sum / samples.Length;
    }
}