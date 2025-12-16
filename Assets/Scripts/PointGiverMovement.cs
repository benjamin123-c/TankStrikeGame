using UnityEngine;

public class PointGiverMovement : MonoBehaviour
{
    private float speed;
    private Transform destroyPoint;

    public void Init(float fallSpeed, Transform destroy)
    {
        speed = fallSpeed;
        destroyPoint = destroy;
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < destroyPoint.position.y)
            Destroy(gameObject);
    }
}