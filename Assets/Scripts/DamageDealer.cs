using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageDealer : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damage = 2;                   // Default = 2 (for Wave 1)

    // Optional: Visual help in Inspector
    [Tooltip("Set damage based on wave:\nWave1=2, Wave2=4, Wave3=6, Wave4=8\nBullets=1")]
    public bool isBullet = false;

    private void Reset()
    {
        // Automatically set damage when script is added in Editor
        if (isBullet)
            damage = 1;
        else
            damage = 2;
    }

    // Call this method when spawning to set correct damage
    public void SetDamageByWave(int waveNumber)
    {
        if (isBullet)
        {
            damage = 1;
        }
        else
        {
            damage = waveNumber * 2;  // Wave 1 → 2, Wave 2 → 4, etc.
        }
    }
}