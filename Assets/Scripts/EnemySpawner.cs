using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;   // Needed for SceneManager

public class EnemySpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] List<WaveConfig> waveConfigs;   // Your ScriptableObject
    [SerializeField] bool looping = true;

    private int currentWaveIndex = 0;

    // Reference to player (optional, for future bullet homing)
    public Transform playerTransform;

    IEnumerator Start()
    {
        do
        {
            WaveConfig currentWave = waveConfigs[currentWaveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));

            // Move to next wave
            currentWaveIndex++;
            if (currentWaveIndex >= waveConfigs.Count)
            {
                currentWaveIndex = 0; // Loop back — required by assignment
            }

            yield return new WaitForSeconds(2f); // Delay between waves

        } while (looping);
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig wave)
    {
        for (int i = 0; i < wave.GetNumberOfEnemies(); i++)
        {
            // Instantiate the path prefab and get its waypoints
            GameObject pathInstance = Instantiate(wave.pathPrefab, Vector3.zero, Quaternion.identity);

            // Get the first waypoint position
            Vector3 startPos = wave.GetPathPrefab()[0].position;

            // Spawn the enemy
            GameObject enemy = Instantiate(
                wave.GetEnemyPrefab(),
                startPos,
                Quaternion.identity);

            // === Set correct damage (Task 2c) ===
            DamageDealer damageDealer = enemy.GetComponent<DamageDealer>();
            if (damageDealer != null)
            {
                int waveNumber = currentWaveIndex + 1; // Wave 1 = index 0 → waveNumber 1
                damageDealer.SetDamageByWave(waveNumber); // Sets 2, 4, 6, 8
            }

            // === Set up movement using YOUR existing EnemyPathing script ===
            EnemyPathing enemyPathing = enemy.GetComponent<EnemyPathing>();
            if (enemyPathing != null)
            {
                enemyPathing.SetWaveConfig(wave); // This gives it access to path & speed
            }

            // === Level 2: Add bullet shooting ===
            if (SceneManager.GetActiveScene().name.Contains("Level2") || 
                SceneManager.GetActiveScene().name == "Level2")
            {
                if (enemy.GetComponent<BulletShooter>() == null)
                {
                    enemy.AddComponent<BulletShooter>();
                }
            }

            // Wait before spawning next enemy in this wave
            yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
        }
    }
}