using UnityEngine;

public class Layer4Textureoffset : MonoBehaviour
{
    public Renderer targetRenderer;
    public float speed = 0.1f;

    private Material material;

    void Start()
    {
        if (targetRenderer != null)
        {
            material = targetRenderer.material; // This creates a new instance of the material
        }
        else
        {
            Debug.LogError("No renderer assigned.");
        }
    }

    void Update()
    {
        if (material != null)
        {
            float offsetX = Time.time * -speed; // Negate the speed to animate in the opposite direction
            material.SetTextureOffset("_BaseMap", new Vector2(offsetX, 0));
            Debug.Log("OffsetX: " + offsetX); // Debug log to check the offset value
        }
    }
}
