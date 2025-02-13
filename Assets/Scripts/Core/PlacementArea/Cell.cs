using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Material hoverMaterial;
    public Vector3 positionOffset;
    private Renderer cellRenderer;
    private Material defaultMaterial;
    private GameObject tower;

    private void Start()
    {
        cellRenderer = GetComponent<Renderer>();
        defaultMaterial = cellRenderer.material;
    }

    private void OnMouseDown()
    {
        if (tower != null)
        {
            Debug.Log("Cell is occupied");
            return;
        }

        GameObject towerPrefab = TowerManager.Instance.GetTowerToPlace();
        if (towerPrefab == null)
        {
            Debug.Log("No tower selected!");
            return;
        }

        tower = Instantiate(towerPrefab, transform.position + positionOffset, Quaternion.identity);
        Debug.Log($"Tower placed at: {transform.position}");
    }

    private void OnMouseEnter()
    {
        cellRenderer.material = hoverMaterial;
    }

    private void OnMouseExit()
    {
        cellRenderer.material = defaultMaterial;
    }

    public bool IsOccupied()
    {
        return tower != null;
    }

    public void SetTower(GameObject newTower)
    {
        tower = newTower;
    }
}