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

            if (config.pointGiverPrefab != null && bottomDestroyPoint != null)
            {
                GameObject pg = Instantiate(config.pointGiverPrefab, transform.position, Quaternion.identity);
                PointGiverMovement mover = pg.GetComponent<PointGiverMovement>();
                if (mover == null)
                    mover = pg.AddComponent<PointGiverMovement>();
                mover.Init(5f, bottomDestroyPoint.transform);
                spawnedCount++;
                Debug.Log("Point giver spawned! Count: " + spawnedCount + " | Position: " + transform.position);
            }
            else
            {
                Debug.LogError("Missing: config.pointGiverPrefab or bottomDestroyPoint!");
            }

            // Level transition ONLY after ALL 10 spawned
            if (spawnedCount >= totalToSpawn)
            {
                yield return new WaitForSeconds(4f);  // Let last one fall
                if (LevelManager.Instance != null)
                    LevelManager.Instance.On10thPointGiverCollected();
                yield break;  // Stop spawning
            }
        }
    }
}