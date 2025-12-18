using UnityEngine;

public class PointGiverMovement : MonoBehaviour
{
    private float speed = 5f;
    private Transform destroyPoint;

    public void Init(float fallSpeed, Transform dp)
    {
        speed = fallSpeed;
        destroyPoint = dp;  // This was null before
    }

    void Update()
    {
        if (destroyPoint == null) return;  // Safety check

        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < destroyPoint.position.y)
        {
            Destroy(gameObject);
        }
    }
}