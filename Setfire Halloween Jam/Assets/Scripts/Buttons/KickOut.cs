using UnityEngine;

public class KickOut : MonoBehaviour
{
    [SerializeField] private Animator timerAnimator;
    [SerializeField] private GameObject KickCanvas;
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
        if (!npc.GetComponent<NPCInteraction>().imposter) timerAnimator.speed++;
        else NPCSpawn.Instance.currentImpostors--;
        Destroy(npc);
        interactionCanvas.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
        KickCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
