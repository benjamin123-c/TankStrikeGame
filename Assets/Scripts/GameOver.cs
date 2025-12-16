using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "GAME OVER! Final Score: " + LevelManager.Instance.score;
    }
}