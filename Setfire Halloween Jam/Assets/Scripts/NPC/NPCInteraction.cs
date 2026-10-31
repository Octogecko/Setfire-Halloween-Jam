using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private Sprite fileSprite;
    [SerializeField] private string fileDialogue;
    [SerializeField] private bool imposter;

    private GameObject interactionCanvas;
    private GameObject player;

    private void Awake()
    {
        interactionCanvas = GameObject.FindGameObjectWithTag("Interaction");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        interactionCanvas.SetActive(false);
    }

    public void Interact()
    {
        if (interactionCanvas.activeSelf)
        {
            interactionCanvas.SetActive(false);
            player.GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            interactionCanvas.transform.GetChild(1).GetComponent<Image>().sprite = fileSprite;
            interactionCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = fileDialogue;
            interactionCanvas.transform.GetChild(3).GetComponent<KickOut>().npc = gameObject;
            interactionCanvas.SetActive(true);
            player.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
