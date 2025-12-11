using UnityEngine;

[CreateAssetMenu(fileName = "PointGiverConfig", menuName = "ScriptableObjects/PointGiverConfig")]
public class PointGiverConfig : ScriptableObject
{
    public GameObject pointGiverPrefab;
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 5f;
}