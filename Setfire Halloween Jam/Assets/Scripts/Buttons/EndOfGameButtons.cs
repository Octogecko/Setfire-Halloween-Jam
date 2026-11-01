using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGameButtons : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("House");
    }

    public void Leave()
    {
        Application.Quit();
    }
}
