using UnityEngine;
using UnityEngine.SceneManagement;

public class KickOut : MonoBehaviour
{
    [SerializeField] private Animator timerAnimator;
    [SerializeField] private GameObject KickCanvas;
    [SerializeField] private GameObject blackScreen;
    private GameObject interactionCanvas;
    private GameObject player;
    public GameObject npc;

    private void Awake()
    {
        interactionCanvas = GameObject.FindGameObjectWithTag("Interaction");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void KickNPCOut()
    {
        player.GetComponent<SoundManager>().PlayAudio(Sounds.KICK);
        Cursor.visible = false;
        if (!npc.GetComponent<NPCInteraction>().imposter)
        {
            timerAnimator.speed++;
            player.GetComponent<SoundManager>().PlayAudio(Sounds.KICK_NON_IMPOSTOR);
        }
        else
        {
            NPCSpawn.Instance.currentImpostors--;
            player.GetComponent<SoundManager>().PlayAudio(Sounds.KICK_IMPOSTOR);
        }
        Destroy(npc);
        interactionCanvas.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;

        if (NPCSpawn.Instance.currentImpostors == 0)
        {
            blackScreen.SetActive(true);
            Invoke("LoadWinScene", 1f);
        }
        else
        {
            KickCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void LoadWinScene()
    {
        SceneManager.LoadScene("Win");
    }
}
