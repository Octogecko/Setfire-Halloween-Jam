using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private Sprite fileSprite;
    [SerializeField] private string fileDialogue;

    private GameObject interactionCanvas;
    private GameObject player;

    private void Start()
    {
        interactionCanvas = GameObject.FindGameObjectWithTag("Interaction");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interact()
    {
        if (interactionCanvas.activeSelf)
        {
            interactionCanvas.SetActive(false);
        }
        else
        {

            interactionCanvas.SetActive(true);
        }
    }
}
