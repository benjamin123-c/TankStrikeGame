using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Player Stats")]
    public int health = 100;
    public int score = 0;

    [Header("UI References (Drag in Level1 & Level2)")]
    public Text healthText;
    public Text scoreText;

    [Header("Scenes Names")]
    public string level1Scene = "Level1";
    public string level2Scene = "Level2";
    public string gameOverScene = "GameOver";
    public string winScene = "Win";

    private void Awake()
    {
        // Singleton + persist between scenes
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

    public void LoadLevel2()
    {
        SceneManager.LoadScene(level2Scene);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(gameOverScene);
    }

    public void WinGame()
    {
        SceneManager.LoadScene(winScene);
    }

    void UpdateUI()
    {
        if (healthText) healthText.text = "Health: " + health;
        if (scoreText) scoreText.text = "Score: " + score;
    }

    // Call this when 10th point-giver is collected
    public void On10thPointGiverCollected()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
            SceneManager.LoadScene("Level2");
        else
            SceneManager.LoadScene("Win");
    }
}