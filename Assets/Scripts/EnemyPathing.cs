using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    public void SetWaveConfig(WaveConfig waveConfigToSet)
    {
        this.waveConfig = waveConfigToSet;
        waypoints = waveConfig.GetPathPrefab();

        if (waypoints != null && waypoints.Count > 0)
        {
            transform.position = waypoints[0].position;  // Start at first waypoint
            waypointIndex = 1;  // Next target = waypoint 1
        }
    }

    void Update()
    {
        if (waveConfig == null || waypoints == null || waypointIndex >= waypoints.Count)
        {
            Destroy(gameObject);  // Safe destroy if null or finished
            return;
        }

        Vector3 targetPosition = waypoints[waypointIndex].position;
        float speedThisFrame = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speedThisFrame);

        if (transform.position == targetPosition)
            waypointIndex++;
    }
}