using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawCircle : MonoBehaviour
{
    public int segments = 50; // Nombre de points du cercle
    public float radius = 1f; // Rayon de base (sera mis à jour)
    private LineRenderer lineRenderer;

    void Awake() // Assurer l'initialisation dès l'activation
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;
        Draw();
    }

    public void SetRadius(float newRadius)
    {
        if (lineRenderer == null) 
        {
            lineRenderer = GetComponent<LineRenderer>();
            if (lineRenderer == null)
            {
                Debug.LogError("LineRenderer is missing on DrawCircle object!", this);
                return;
            }
        }

        radius = newRadius;
        Draw();
        transform.localPosition = new Vector3(0, 0.7f, 0); 
    }

    private void Draw()
    {
        if (lineRenderer == null) return;

        lineRenderer.positionCount = segments + 1;

        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));
            angle += 2 * Mathf.PI / segments;
        }
    }
}
