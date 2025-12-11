using UnityEngine;
using System.Collections.Generic;

public class Path : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();

    void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints.Add(transform.GetChild(i));
        }
    }
}