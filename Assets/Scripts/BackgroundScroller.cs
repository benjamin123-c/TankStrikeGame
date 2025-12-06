using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Tooltip("Scrolling speed in Unity units per second")]
    [SerializeField] float scrollSpeed = 0.10f;

    // Private variable to hold the Material
    Material backgroundMaterial;

    Vector2 offset;

    void Start()
    {
        // Get the Renderer component and its material
        // The material property creates an instance copy, so we are modifying only THIS material.
        backgroundMaterial = GetComponent<MeshRenderer>().material;
        offset = new Vector2(0f, scrollSpeed);
    }

    void Update()
    {
        // Calculate the distance to scroll based on time and speed
        // Time.time is the time since the game started
        //float yOffset = Time.time * scrollSpeed;

        // Apply the new offset to the texture's main property (_MainTex)
        // Vector2(x, y) where x is horizontal offset and y is vertical offset
        backgroundMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}