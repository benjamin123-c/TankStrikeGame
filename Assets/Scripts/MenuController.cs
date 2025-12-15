using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Buttons")]
    public Button playButton;
    public Button quitButton;

    [Header("Audio")]
    public AudioSource backgroundMusic;   // Drag your BGM AudioSource here

    private void Start()
    {
        // Start background music (loops forever)
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
            DontDestroyOnLoad(backgroundMusic.gameObject);
        }

        // Button clicks
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void PlayGame()
    {
        SceneManager.LoadScene("Level1");   // Change if your level name is different
    }

    void QuitGame()
    {
        Application.Quit();
        // For testing in Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}