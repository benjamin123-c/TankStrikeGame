using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] List<WaveConfig> waveConfigs;
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
            Vector3 spawnPos = wave.GetPathPrefab()[0].position;

            GameObject enemy = Instantiate(wave.GetEnemyPrefab(), spawnPos, Quaternion.identity);

            DamageDealer dd = enemy.GetComponent<DamageDealer>();
            if (dd != null)
                dd.SetDamageByWave(currentWaveIndex + 1);

            EnemyPathing pathing = enemy.GetComponent<EnemyPathing>();
            if (pathing != null)
                pathing.SetWaveConfig(wave);

            // FIXED: Shooting ONLY in Level2
            if (SceneManager.GetActiveScene().name.Contains("Level2"))
            {
                BulletShooter shooter = enemy.GetComponent<BulletShooter>();
                if (shooter == null)
                    enemy.AddComponent<BulletShooter>();
            }

            yield return new WaitForSeconds(wave.GetTimeBetweenSpawns());
        }
    }
}