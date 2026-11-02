using TMPro;
using UnityEngine;

public class LoseText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text += "\nThere were " + GameStateManager.Instance.impostorsLeft + " impostors left";
    }
}
