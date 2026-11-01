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

        int themeChoice = Random.Range(0, 1);
        theme = themeChoice == 0 ? ThemesEnum.PUMPKIN : ThemesEnum.GHOST; // Premier niveau
        outOfTheme = themeChoice == 1 ? ThemesEnum.PUMPKIN : ThemesEnum.GHOST;
    }

    public void EndGame()
    {
        Debug.Log("boom boom explosion sounds the game has ended");
    }
}
