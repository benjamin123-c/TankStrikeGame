using System.Collections;
using UnityEngine;

public class PointGiverSpawner : MonoBehaviour
{
    [Header("Config")]
    public PointGiverConfig config;
    public int totalToSpawn = 10;
    public GameObject bottomDestroyPoint;

    private int spawnedCount = 0;

    IEnumerator Start()
    {
        for (int i = 0; i < totalToSpawn; i++)
        {
            float delay = Random.Range(config.minSpawnTime, config.maxSpawnTime);
            yield return new WaitForSeconds(delay);

            if (config.pointGiverPrefab != null)
            {
                GameObject pg = Instantiate(config.pointGiverPrefab, transform.position, Quaternion.identity);
                PointGiverMovement mover = pg.AddComponent<PointGiverMovement>();
                mover.Init(5f, bottomDestroyPoint.transform);
                spawnedCount++;
                Debug.Log("Point giver spawned! Count: " + spawnedCount);
            }
            else
            {
                Debug.LogError("PointGiverPrefab is NULL – assign in PointGiverSettings!");
            }

            if (spawnedCount == totalToSpawn)
            {
                yield return new WaitForSeconds(4f);
                if (LevelManager.Instance != null)
                    LevelManager.Instance.On10thPointGiverCollected();
            }
        }
    }
}