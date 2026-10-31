using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private Sprite fileSprite;
    [SerializeField] private string fileDialogue;

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
        }
        else
        {
            interactionCanvas.transform.GetChild(1).GetComponent<Image>().sprite = fileSprite;
            interactionCanvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = fileDialogue;
            interactionCanvas.SetActive(true);
        }
    }
}
