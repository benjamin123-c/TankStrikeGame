using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] List<WaveConfig> waveConfigs;   // ← Fixed: added <WaveConfig>
    [SerializeField] bool looping = true;

    private int currentWaveIndex = 0;

    IEnumerator Start()
    {
        do
        {
            WaveConfig currentWave = waveConfigs[currentWaveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));

            currentWaveIndex++;
            if (currentWaveIndex >= waveConfigs.Count)
                currentWaveIndex = 0;

            yield return new WaitForSeconds(2f);
        } while (looping);
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig wave)
    {
        for (int i = 0; i < wave.GetNumberOfEnemies(); i++)
        {
            Vector3 startPos = wave.GetPathPrefab()[0].position;

            GameObject enemy = Instantiate(wave.GetEnemyPrefab(), startPos, Quaternion.identity);

            // Damage - Fixed
            var dd = enemy.GetComponent<DamageDealer>();
            if (dd != null)
                dd.SetDamageByWave(currentWaveIndex + 1);

            // Pathing - Fixed
            EnemyPathing pathing = enemy.GetComponent<EnemyPathing>();
            if (pathing != null)
                pathing.SetWaveConfig(wave);

            // Level 2 shooting - Fixed (ONLY activate in Level2 scene)
            if (SceneManager.GetActiveScene().name == "Level2")
            {
                BulletShooter shooter = enemy.GetComponent<BulletShooter>();
                if (shooter == null)
                    enemy.AddComponent<BulletShooter>();
            }

            yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
        }
    }
}