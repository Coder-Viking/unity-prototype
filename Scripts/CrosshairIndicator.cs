using UnityEngine;

public class CrosshairIndicator : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;
    private SpriteRenderer spriteRenderer;

    // Initializes the crosshair indicator with a target, color and offset
    public void Initialize(Transform target, Color color, Vector3 offset)
    {
        this.target = target;
        this.offset = offset;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }

    private void LateUpdate()
    {
        // If the target has been destroyed, also destroy the crosshair indicator
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Keep the crosshair positioned above the target with the given offset
        transform.position = target.position + offset;

        // Optionally, you could orient the crosshair towards the camera here if desired
    }
}
