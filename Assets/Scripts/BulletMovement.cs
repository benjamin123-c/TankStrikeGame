using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Vector3 targetPosition;
    float speed = 5f;

    public void Init(Vector3 target, float bulletSpeed)
    {
        targetPosition = target;
        speed = bulletSpeed;
    }

    void Update()
    {
        // Move toward target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Destroy if off screen
        if (transform.position.y < -12f)
            Destroy(gameObject);
    }
}