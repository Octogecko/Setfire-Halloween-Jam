using UnityEngine;

public class KickOut : MonoBehaviour
{
    [SerializeField] private Animator timerAnimator;
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
        Cursor.visible = false;
        if (!npc.GetComponent<NPCInteraction>().imposter) timerAnimator.Update(5);
        else NPCSpawn.Instance.impostorAmount--;
        Destroy(npc);
        interactionCanvas.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
