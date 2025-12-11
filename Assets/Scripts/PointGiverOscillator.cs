using System.Collections;
using UnityEngine;

public class PointGiverSpawner : MonoBehaviour
{
    public PointGiverConfig config;
    public int totalToSpawn = 10;
    public GameObject bottomDestroy; // Empty at y=-11
    private int spawned = 0;
    private bool isLevel1 = true;

    void Start()
    {
        StartCoroutine(SpawnPointGivers());
    }

    IEnumerator SpawnPointGivers()
    {
        for (int i = 0; i < totalToSpawn; i++)
        {
            float delay = Random.Range(config.minSpawnTime, config.maxSpawnTime);
            yield return new WaitForSeconds(delay);
            GameObject pg = Instantiate(config.pointGiverPrefab, transform.position, Quaternion.identity);
            pg.AddComponent<PointGiverMovement>().Init(5f, bottomDestroy.transform);
            spawned++;
            if (spawned == totalToSpawn && isLevel1)
            {
                yield return new WaitForSeconds(5f); // Wait for last to fall
                LevelManager.Instance.LoadScene("Level2");
            }
        }
    }
}