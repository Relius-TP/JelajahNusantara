using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;

    private void OnEnable()
    {
        Interactable.OnSuccesGetKey += PlayOneShot;
    }

    private void PlayOneShot(AudioClip clip)
    {
        m_audioSource.PlayOneShot(clip);
    }
}
