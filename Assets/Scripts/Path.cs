using UnityEngine;
using System.Collections.Generic;

// Note: Ensure this script is in your Assets/Scripts folder

public class Path : MonoBehaviour
{
    // Public List to hold the Waypoint Transforms (from children GameObjects)
    // The [SerializeField] makes it visible in the Inspector without being public
    [Tooltip("The ordered list of waypoint Transforms for this path.")]
    [SerializeField] List<Transform> waypoints = new List<Transform>();

    // Public property to easily access the list from other scripts
    public List<Transform> Waypoints => waypoints;

    void Awake()
    {
        // Automatically populate the list with child transforms
        // This makes creating the path easy: just drag empty GameObjects under the Path object.
        waypoints.Clear();
        foreach (Transform child in transform)
        {
            waypoints.Add(child);
        }
    }
}