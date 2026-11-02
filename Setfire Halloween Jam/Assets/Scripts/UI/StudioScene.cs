using UnityEngine;
using UnityEngine.SceneManagement;

public class StudioScene : MonoBehaviour
{
    private void Start()
    {
        Invoke("GoToNextScene", 2f);
    }

    private void GoToNextScene()
    {
        SceneManager.LoadScene("Main menu");
    }
}
