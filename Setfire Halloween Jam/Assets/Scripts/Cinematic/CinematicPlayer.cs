using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // supprime si tu utilises l'ancien Input Manager

public class CinematicPlayer : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "MainMenu";
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        // Permettre de passer la cinématique (Échap ou Espace)
        if (Keyboard.current.escapeKey.wasPressedThisFrame ||
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            LoadNext();
        }
    }

    void OnVideoEnd(VideoPlayer vp) => LoadNext();

    void LoadNext()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
        SceneManager.LoadScene(nextSceneName);
    }
}