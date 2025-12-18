using UnityEngine;

public class PointGiverMovement : MonoBehaviour
{
    private float speed = 5f;
    private Transform destroyPoint;

    public void Init(float s, Transform dp)
    {
        speed = s;
        destroyPoint = dp;
    }

    void Update()
    {
        // Safety check – if destroyPoint missing, destroy object but don't crash
        if (destroyPoint == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < destroyPoint.position.y)
        {
            Destroy(gameObject);
        }
    }
}