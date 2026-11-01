using UnityEngine;

public class EndOfTimer : MonoBehaviour
{
    void Start()
    {
        GameStateManager.Instance.EndGame();
    }
}
