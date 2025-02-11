using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    private Renderer cellRenderer;
    private void Start()
    {
        cellRenderer = GetComponent<Renderer>();

    }

    private void OnMouseEnter()
    {
        cellRenderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        Debug.Log("Exit Cell");
    }
}