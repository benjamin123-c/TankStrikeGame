using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 7f;

    private Camera mainCamera;
    private float minX, maxX;

    [Header("Audio & Effects")]
    public AudioClip hitSound;           // Drag hit.wav here
    public AudioClip collectSound;       // Drag collect.wav here
    public AudioClip deathSound;         // NEW: Drag death/explosion sound here
    public GameObject explosionPrefab;   // Drag explosion particle prefab here

    private AudioSource audioSource;

    void Start()
    {
        // Camera border calculation
        mainCamera = Camera.main;
        float playerWidth = transform.localScale.x / 2f;
        minX = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + playerWidth;
        maxX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - playerWidth;

        // Audio setup
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Movement
        float inputX = Input.GetAxis("Horizontal");
        Vector3 moveVector = Vector3.right * inputX * moveSpeed * Time.deltaTime;
        transform.position += moveVector;

        // Clamp to borders
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Damage from enemy or bullet
        DamageDealer dd = other.GetComponent<DamageDealer>();
        if (dd != null)
        {
            LevelManager.Instance.TakeDamage(dd.damage);
            audioSource.PlayOneShot(hitSound);

            if (explosionPrefab != null)
                Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);

            Destroy(other.gameObject);

            // NEW: Check if player died after this hit
            if (LevelManager.Instance.health <= 0)
            {
                PlayerDied();
            }

            return;
        }

        // Collect point-giver
        if (other.CompareTag("PointGiver"))
        {
            LevelManager.Instance.AddScore(5);
            audioSource.PlayOneShot(collectSound);
            Destroy(other.gameObject);
        }
    }

    // NEW METHOD: Handle player death
    private void PlayerDied()
    {
        // Play death sound
        if (deathSound != null)
            audioSource.PlayOneShot(deathSound);

        // Big explosion on player
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Destroy the player tank
        Destroy(gameObject);
    }
}