using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameButtons : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("House");
        Destroy(GameStateManager.Instance.gameObject);
    }

    public void Leave()
    {
        Application.Quit();
    }
}
