using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    private void Start()
    {
        Instance = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void EndGame()
    {
        Debug.Log("boom boom explosion sounds the game has ended");
    }
}
