using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;               // TextMeshPro
using UnityEngine.UI;      // Legacy Text

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Player Stats")]
    public int health = 100;
    public int score = 0;

    [Header("UI References")]
    public TextMeshProUGUI healthTextTMP;   // For TextMeshPro "New Text"
    public TextMeshProUGUI scoreTextTMP;
    public Text healthTextLegacy;           // For legacy Text (if you used it)
    public Text scoreTextLegacy;

    [Header("Scene Names")]
    public string level1Scene = "Level1";
    public string level2Scene = "Level2";
    public string gameOverScene = "GameOver";
    public string winScene = "Win";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateUI();

        if (health <= 0)
        {
            health = 0;
            GameOver();
        }
    }

    public void On10thPointGiverCollected()
    {
        string current = SceneManager.GetActiveScene().name;
        if (current == level1Scene || current.Contains("Level1"))
            SceneManager.LoadScene(level2Scene);
        else
            SceneManager.LoadScene(winScene);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(gameOverScene);
    }

    public void WinGame()
    {
        SceneManager.LoadScene(winScene);
    }

    private void UpdateUI()
    {
        // TextMeshPro
        if (healthTextTMP != null)
            healthTextTMP.text = "Health: " + health;
        if (scoreTextTMP != null)
            scoreTextTMP.text = "Score: " + score;

        // Legacy Text (backup)
        if (healthTextLegacy != null)
            healthTextLegacy.text = "Health: " + health;
        if (scoreTextLegacy != null)
            scoreTextLegacy.text = "Score: " + score;
    }
}