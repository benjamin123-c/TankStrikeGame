using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public int health = 100;
    public int score = 0;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); } else Destroy(gameObject);
    }

    public void LoadScene(string name) { SceneManager.LoadScene(name); }
    public void UpdateHealth(int delta) { health += delta; if (health <= 0) LoadScene("GameOver"); }
    public void AddScore(int points) { score += points; }
}