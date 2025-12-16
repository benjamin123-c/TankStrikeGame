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
    public GameObject explosionPrefab;   // Drag your explosion particle prefab here

    private AudioSource audioSource;

    void Start()
    {
        // Camera border calculation (your original code)
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
        // Your original movement code
        float inputX = Input.GetAxis("Horizontal");
        Vector3 moveVector = Vector3.right * inputX * moveSpeed * Time.deltaTime;
        transform.position += moveVector;

        // Clamp to camera borders
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Damage from enemy or bullet (Task 3c & 3d)
        DamageDealer dd = other.GetComponent<DamageDealer>();
        if (dd != null)
        {
            LevelManager.Instance.TakeDamage(dd.damage);
            audioSource.PlayOneShot(hitSound);

            // Explosion effect on enemy/bullet
            if (explosionPrefab != null)
                Instantiate(explosionPrefab, other.transform.position, Quaternion.identity);

            Destroy(other.gameObject);   // Destroy enemy or bullet
            return;
        }

        // Collect point-giver (Task 3e)
        if (other.CompareTag("PointGiver"))
        {
            LevelManager.Instance.AddScore(5);
            audioSource.PlayOneShot(collectSound);
            Destroy(other.gameObject);
        }
    }
}