using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveConfig", menuName = "Scriptable Objects/WaveConfig")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] GameObject pathPrefab;

    [SerializeField] float timeBetweenSpawns = 0.5f;

    [SerializeField] float enemyMoveSpeed = 2f;

    [SerializeField] int numberOfEnemies = 5;

    //encapsulation methods -- give variables read-only access
    public GameObject GetEnemyPrefab()
    { 
        return enemyPrefab;
    }

    public float GetTimeBetweenSpawns()
    { 
        return timeBetweenSpawns;
    }

    public float GetEnemyMoveSpeed()
    { 
        return enemyMoveSpeed;
    }

    public int GetNumberOfEnemies()
    { 
        return numberOfEnemies;
    }

    public List<Transform> GetPathPrefab()
    { 
        List<Transform> waypointsList = new List<Transform>();
        //add each child of the pathPrefab to the waypointsList
        foreach (Transform child in pathPrefab.transform)
        { 
            waypointsList.Add(child);
        }
        return waypointsList;

    }


    
}
