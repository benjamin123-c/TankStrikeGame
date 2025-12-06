using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Scriptable Objects/WaveConfig")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] List<Transform> pathPrefab;

    [SerializeField] float timeBetweenSpawns = 0.5f;

    [SerializeField] float enemyMoveSpeed = 2f;

    [SerializeField] int numberOfEnemies = 5;
    
}
