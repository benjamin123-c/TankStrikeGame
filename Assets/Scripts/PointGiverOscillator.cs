using UnityEngine;

public class PointGiverOscillator : MonoBehaviour
{
    public float amplitude = 8f;
    public float speed = 0.8f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPos.x + x, transform.position.y, 0);
    }
}