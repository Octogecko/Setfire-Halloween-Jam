using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HelpNPC : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;

    private void Start()
    {
        tmp.text = "Hi! This party's dress code is \"" + GameStateManager.Instance.theme + "\". Please show anyone who didn't follow it the door.I don't need any party killers in here.";
        Invoke("DestroyHelper", 3f);
    }

    private void DestroyHelper()
    {
        Destroy(gameObject);
    }
}
