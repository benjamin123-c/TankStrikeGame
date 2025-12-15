using UnityEngine;
using UnityEngine.SceneManagement;   // ← ADD THIS LINE
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button play, quit;
    public AudioSource bgm;

    void Start()
    {
        play.onClick.AddListener(PlayGame);
        quit.onClick.AddListener(QuitGame);

        if (bgm != null)
        {
            bgm.Play();
            DontDestroyOnLoad(bgm.gameObject);
        }
    }

    void PlayGame()
    {
        SceneManager.LoadScene("Level1");   // ← Direct load – no need for LevelManager here
    }

    void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}