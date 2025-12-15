using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
        waypoints = waveConfig.GetPathPrefab();           // Get all waypoints from the path prefab
        transform.position = waypoints[0].position;       // Start at first waypoint
        waypointIndex = 1;                                // Next target = index 1
    }

    void Update()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float speedThisFrame = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speedThisFrame);

            if (transform.position == targetPosition)
                waypointIndex++;
        }
        else
        {
            // Reached the end → destroy (or damage player if you want)
            Destroy(gameObject);
        }
    }
}