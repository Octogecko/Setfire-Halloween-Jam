using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    public ThemesEnum theme;
    public ThemesEnum outOfTheme;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        int themeChoice = Random.Range(0, 2);
        theme = themeChoice == 0 ? ThemesEnum.PUMPKIN : ThemesEnum.GHOST; // Premier niveau
        outOfTheme = themeChoice == 1 ? ThemesEnum.PUMPKIN : ThemesEnum.GHOST;
    }

    public void EndGame()
    {
        int impostorsLeft = NPCSpawn.Instance.currentImpostors;

        if (impostorsLeft > 0)
        {
            SceneManager.LoadScene("Loss");
        }
        else
        {
            SceneManager.LoadScene("Win");
        }
    }
}
