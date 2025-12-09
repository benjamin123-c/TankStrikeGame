using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigList;

    int startingWave = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //get the current wave configuration
        WaveConfig currentWave = waveConfigList[startingWave];

        //start the coroutine to spawn all enemies in the current wave
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //coroutine to spawn all enemies in a wave
    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        //spawn enemies based on the number specified in the wavwConfig
        for (int enemyCount = 1; enemyCount <= waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            //spawn an enemy at the position of the first waypoint in the pathPrefab
            Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetPathPrefab()[0].transform.position,
                Quaternion.identity);
            //wait for the specified time before spawning the next enemy
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());

        }
    }
}
