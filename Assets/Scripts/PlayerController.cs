using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    // Player movement speed in Unity units per second
    [SerializeField] float moveSpeed = 7f;

    // Cached camera reference for border calculations
    private Camera mainCamera;
    private float minX, maxX; // World coordinates for camera borders

    void Start()
    {
        // Get the Main Camera
        mainCamera = Camera.main;

        // Calculate the boundary limits in world space
        // This accounts for the player's width (half the scale in world space)
        float playerWidth = transform.localScale.x / 2f;

        // ViewportToWorldPoint(0, 0) is bottom-left (x=minX)
        // ViewportToWorldPoint(1, 0) is bottom-right (x=maxX)

        // Calculate world bounds, subtracting/adding the player's half-width
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + playerWidth;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - playerWidth;
    }

    void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        // KU 3.3 requirement: Use built-in Unity methods (GetAxis)
        float inputX = Input.GetAxis("Horizontal");

        // Calculate the movement vector
        // Use Time.deltaTime (KU 3.3 requirement) for frame-rate independence
        Vector3 moveVector = Vector3.right * inputX * moveSpeed * Time.deltaTime;

        // Apply movement
        transform.position += moveVector;

        // Clamp the player's position to stay within the camera borders (KU 3.3 requirement)
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);

        // Apply the clamped position
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}