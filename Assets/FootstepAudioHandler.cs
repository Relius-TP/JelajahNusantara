using UnityEngine;

public class FootstepAudioHandler : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerMovement.OnWalk += PlaySound;
    }

    private void OnDisable()
    {
        PlayerMovement.OnWalk -= PlaySound;
    }

    private void PlaySound(bool state)
    {
        if (state)
        {
            m_AudioSource.Play();
        }
        else
        {
            m_AudioSource.Stop();
        }
        
    }
}
