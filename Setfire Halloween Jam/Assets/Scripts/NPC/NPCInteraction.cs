using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private Sprite fileSprite;
    [SerializeField] private string fileDialogue;
    public bool imposter;

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
            Cursor.visible = false;
            interactionCanvas.SetActive(false);
            player.GetComponent<PlayerMovement>().enabled = true;
        }
        else
        {
            Cursor.visible = true;
            interactionCanvas.transform.GetChild(4).GetComponent<Image>().sprite = fileSprite;
            interactionCanvas.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = fileDialogue;
            interactionCanvas.transform.GetChild(6).GetComponent<KickOut>().npc = gameObject;
            interactionCanvas.SetActive(true);
            player.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
