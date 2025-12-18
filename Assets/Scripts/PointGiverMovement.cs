using System.Collections;
using UnityEngine;

public class PointGiverMovement : MonoBehaviour
{
    float speed = 5f;
    Transform destroyPoint;

    public void Init(float s, Transform dp)
    {
        speed = s;
        destroyPoint = dp;
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < destroyPoint.position.y)
            Destroy(gameObject);
    }
}