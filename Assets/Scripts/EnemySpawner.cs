using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigList;

    int startingWave = 0;

    [SerializeField] bool looping = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   IEnumerator Start()
   {
        do
        {
            //get the current wave configuration
            WaveConfig currentWave = waveConfigList[startingWave];

            //start the coroutine to spawn waves
            yield return StartCoroutine(SpawnAllWaves());
        }
        // when coroutine finishes, check  if looping is true to restart
        while(looping);
        
   }

    // Update is called once per frame
    void Update()
    {
        
    }

    //create a coroutine to spawn all waves
    IEnumerator SpawnAllWaves()
    {
        foreach (WaveConfig waveConfig in waveConfigList)
        {
            //start the coroutine to spawn all enemies in the wave
            yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfig));
        }
           
        
    }

    //coroutine to spawn all enemies in a wave
    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        //spawn enemies based on the number specified in the wavwConfig
        for (int enemyCount = 1; enemyCount <= waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            //spawn an enemy at the position of the first waypoint in the pathPrefab
            GameObject newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetPathPrefab()[0].transform.position,
                Quaternion.identity);

            //set the waveConfig for the newly spawned enemy
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            //wait for the specified time before spawning the next enemy
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());

        }
    }
}
