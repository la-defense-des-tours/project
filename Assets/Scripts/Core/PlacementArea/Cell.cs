using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Material hoverMaterial;
    private Renderer cellRenderer;
    private Material defaultMaterial;
    private void Start()
    {
        cellRenderer = GetComponent<Renderer>();
        defaultMaterial = cellRenderer.material;
    }

    private void OnMouseEnter()
    {
        cellRenderer.material = hoverMaterial;
    }

    private void OnMouseExit()
    {
        cellRenderer.material = defaultMaterial;
    }
}