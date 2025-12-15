using System.Collections;
using UnityEngine;

public class PointGiverSpawner : MonoBehaviour
{
    [Header("Settings")]
    public int PointGiverConfig config;           // Drag your PointGiverSettings here
    public int totalToSpawn = 10;
    public GameObject bottomDestroyPoint;     // Empty GameObject at Y = -12

    private int spawnedCount = 0;

    IEnumerator Start()
    {
        
        while (spawnedCount < totalToSpawn)
        {
            float delay = Random.Range(config.minSpawnTime, config.maxSpawnTime);
            yield return new WaitForSeconds(delay);
            print(spawnedCount);
            GameObject pg = Instantiate(config.pointGiverPrefab, transform.position, Quaternion.identity);
            print(pg.gameObject.name);

            // Make it fall down
            var mover = pg.AddComponent<PointGiverMovement>();
            mover.Init(5f, bottomDestroyPoint.transform);

            spawnedCount++;

            // After the 10th one → go to next level
            if (spawnedCount == totalToSpawn)
            {
                yield return new WaitForSeconds(3f);
                LevelManager.Instance.On10thPointGiverCollected();
            }
        }
    }
}