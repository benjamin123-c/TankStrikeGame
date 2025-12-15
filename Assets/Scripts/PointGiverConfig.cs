using UnityEngine;

[CreateAssetMenu(fileName = "New PointGiver Config", menuName = "ScriptableObjects/PointGiver Config")]
public class PointGiverConfig : ScriptableObject
{
    [Header("What to Spawn")]
    public GameObject pointGiverPrefab;

    [Header("Spawn Timing")]
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 6f;
}