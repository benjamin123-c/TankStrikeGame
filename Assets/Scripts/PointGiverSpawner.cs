using System.Collections;
using UnityEngine;

public class PointGiverSpawner : MonoBehaviour
{
    [Header("Config")]
    public PointGiverConfig config;           // Drag PointGiverSettings here

    [Header("Spawn Control")]
    public int totalToSpawn = 10;             // Exactly 10 as required
    public GameObject bottomDestroyPoint;     // Empty at Y = -12

    private int spawnedCount = 0;

    // TASK 2g: Another coroutine with timer
    IEnumerator Start()
    {
        // Proper loop for exactly 10 spawns
        for (int i = 0; i < totalToSpawn; i++)
        {
            // Random time between min and max (from config)
            float randomDelay = Random.Range(config.minSpawnTime, config.maxSpawnTime);
            yield return new WaitForSeconds(randomDelay);

            // Spawn at the oscillating spawner's current position
            GameObject pointGiver = Instantiate(
                config.pointGiverPrefab,
                transform.position,          // Uses the moving position (Task 2d)
                Quaternion.identity);

            Debug.Log("Point giver spawned! Count: " + spawnedCount);

            // Add falling movement
            PointGiverMovement mover = pointGiver.AddComponent<PointGiverMovement>();
            mover.Init(5f, bottomDestroyPoint.transform);   // Fall speed 5

            spawnedCount++;

            // After the 10th spawn → wait a bit then go to Level 2
            if (spawnedCount == totalToSpawn)
            {
                yield return new WaitForSeconds(4f); // Let last one fall
                LevelManager.Instance.On10thPointGiverCollected();
            }
        }
    }
}