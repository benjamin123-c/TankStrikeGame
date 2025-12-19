using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    [Header("UI References")]
    public Button playAgainButton;
    public Button quitButton;
    public TextMeshProUGUI scoreText;  // "Final Score: X"

    void Start()
    {
        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(PlayAgain);
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);

        UpdateScoreText();
    }

    void PlayAgain()
    {
        // Reset LevelManager stats
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.health = 100;
            LevelManager.Instance.score = 0;
        }

        // Reload Level1
        SceneManager.LoadScene("Level1");
    }

    void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void UpdateScoreText()
    {
        if (scoreText != null && LevelManager.Instance != null)
        {
            scoreText.text = "Final Score: " + LevelManager.Instance.score;
        }
    }
}