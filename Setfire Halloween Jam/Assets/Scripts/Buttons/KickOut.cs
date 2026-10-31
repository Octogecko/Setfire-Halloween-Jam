using UnityEngine;

public class KickOut : MonoBehaviour
{
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
        Destroy(npc);
        interactionCanvas.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
