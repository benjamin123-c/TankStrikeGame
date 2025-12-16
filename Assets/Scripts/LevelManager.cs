using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // For TextMeshProUGUI

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Player Stats")]
    public int health = 100;
    public int score = 0;

    [Header("UI References")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;

    [Header("Scene Names")]
    public string level1Scene = "Level1";
    public string level2Scene = "Level2";
    public string gameOverScene = "GameOver";
    public string winScene = "Win";

    private void Awake()
    {
        // Singleton pattern - persists across scenes
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

    // Called when player collects a point-giver
    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    // Called when player takes damage
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

    // Called by PointGiverSpawner after the 10th point-giver
    public void On10thPointGiverCollected()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == level1Scene)
        {
            SceneManager.LoadScene(level2Scene);
        }
        else if (currentScene == level2Scene)
        {
            WinGame();
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

    // Update UI text
    private void UpdateUI()
    {
        if (healthText != null)
            healthText.text = "Health: " + health;

        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}