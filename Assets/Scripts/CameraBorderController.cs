using System.Collections.Generic;
using UnityEngine;

public class CameraBorderController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the main camera and the collider component
        Camera mainCamera = Camera.main;
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();

        // Calculate the camera's boundaries in world space
        // ViewportToWorldPoint converts normalized screen coordinates (0 to 1) 
        // to world coordinates.
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float borderThickness = 0.5f; // Small offset to keep objects slightly inside

        // Define the points for the rectangle (border)
        List<Vector2> points = new List<Vector2>();

        // 1. Bottom-Left
        points.Add(new Vector2(bottomLeft.x + borderThickness, bottomLeft.y + borderThickness));
        // 2. Top-Left
        points.Add(new Vector2(bottomLeft.x + borderThickness, topRight.y - borderThickness));
        // 3. Top-Right
        points.Add(new Vector2(topRight.x - borderThickness, topRight.y - borderThickness));
        // 4. Bottom-Right
        points.Add(new Vector2(topRight.x - borderThickness, bottomLeft.y + borderThickness));
        // 5. Close the loop (Back to Bottom-Left)
        points.Add(new Vector2(bottomLeft.x + borderThickness, bottomLeft.y + borderThickness));

        // Assign the points to the Edge Collider
        edgeCollider.points = points.ToArray();

        // Ensure the collider is static (doesn't move)
        // Note: For a border, you generally want it to affect other moving rigidbodies.
        // It should not have a Rigidbody component itself unless you need it to react to forces.

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
