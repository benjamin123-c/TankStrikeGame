using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;           // TextMeshProUGUI
using UnityEngine.UI;   // Legacy Text (backup)

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Player Stats")]
    public int health = 100;
    public int score = 0;

    [Header("UI References (Assign in Level1 or leave empty for auto-find)")]
    public TextMeshProUGUI healthTextTMP;
    public TextMeshProUGUI scoreTextTMP;
    public Text healthTextLegacy;     // Optional backup for legacy Text
    public Text scoreTextLegacy;

    [Header("Scene Names")]
    public string level1Scene = "Level1";
    public string level2Scene = "Level2";
    public string gameOverScene = "GameOver";
    public string winScene = "Win";

    private void Awake()
    {
        // Singleton pattern – keep one instance across scenes
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

    // Called every time a new scene is loaded
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Auto-find UI when a new scene loads
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindUIInCurrentScene();
        UpdateUI();
    }

    // Find TextMeshProUGUI objects in the current scene (by name)
    private void FindUIInCurrentScene()
    {
        if (healthTextTMP == null || scoreTextTMP == null)
        {
            TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();
            foreach (var t in texts)
            {
                if (t.name.ToLower().Contains("health"))
                    healthTextTMP = t;
                else if (t.name.ToLower().Contains("score"))
                    scoreTextTMP = t;
            }
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
        UpdateUI();

        if (health <= 0)
        {
            GameOver();
        }
    }

    // FIXED: Check health before loading Win
    public void On10thPointGiverCollected()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == level1Scene || currentScene.Contains("Level1"))
        {
            SceneManager.LoadScene(level2Scene);
        }
        else
        {
            // Only win if health > 0
            if (health > 0)
            {
                WinGame();
            }
            else
            {
                GameOver();
            }
        }
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
        if (healthTextTMP != null)
            healthTextTMP.text = "Health: " + health;

        if (scoreTextTMP != null)
            scoreTextTMP.text = "Score: " + score;

        // Legacy Text fallback
        if (healthTextLegacy != null)
            healthTextLegacy.text = "Health: " + health;

        if (scoreTextLegacy != null)
            scoreTextLegacy.text = "Score: " + score;
    }
}