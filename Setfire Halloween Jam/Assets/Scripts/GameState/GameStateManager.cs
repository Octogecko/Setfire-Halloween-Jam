using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    public ThemesEnum theme;
    public ThemesEnum outOfTheme;

    private void Awake()
    {
        Instance = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        theme = ThemesEnum.PUMPKIN; // Premier niveau
        outOfTheme = ThemesEnum.GHOST;
    }

    public void EndGame()
    {
        Debug.Log("boom boom explosion sounds the game has ended");
    }
}
