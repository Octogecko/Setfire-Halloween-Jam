using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip interactSfx, changeChoiceSfx, glowSfx, kickSfx;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.INTERACT:
                audioSource.PlayOneShot(interactSfx);
                break;
            case Sounds.CHANGE_CHOICE:
                audioSource.PlayOneShot(changeChoiceSfx);
                break;
            case Sounds.GLOW:
                audioSource.PlayOneShot(glowSfx);
                break;
            case Sounds.KICK:
                audioSource.PlayOneShot(kickSfx);
                break;
        }
    }
}
