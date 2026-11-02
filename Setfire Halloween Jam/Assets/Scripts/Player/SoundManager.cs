using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip interactSfx, changeChoiceSfx, glowSfx, kickSfx, kickImpostorSfx, kickNonImpostorSfx;

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
            case Sounds.KICK_IMPOSTOR:
                audioSource.PlayOneShot(kickImpostorSfx);
                break;
            case Sounds.KICK_NON_IMPOSTOR:
                audioSource.PlayOneShot(kickNonImpostorSfx);
                break;
        }
    }
}
