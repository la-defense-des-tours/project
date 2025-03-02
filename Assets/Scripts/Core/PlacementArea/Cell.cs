using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Towers;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Material hoverMaterial;
    public Vector3 positionOffset;
    private Renderer cellRenderer;
    private Material defaultMaterial;

    [HideInInspector] public Tower tower;
    public bool isUpgraded = false;
    private TowerManager towerManager;

    private void Start()
    {
        cellRenderer = GetComponent<Renderer>();
        defaultMaterial = cellRenderer.material;
        towerManager = TowerManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private Tower FindTowerInCell()
    {
        Tower foundTower = GetComponentInChildren<Tower>();
        if (foundTower != null)
        {
            return foundTower;
        }
        return null;
    }

    private void OnMouseDown()
    {
        Debug.Log("Cell clicked");

        Tower detectedTower = FindTowerInCell(); 

        if (detectedTower != null)
        {
            Debug.Log("Cell is occupied");
            towerManager.SelectCell(detectedTower);
            return;
        }
        Debug.Log("Cell is not occupied");
        towerManager.TryPlaceTowerOnCell(this);
    }
    public bool IsOccupied()
    {
        return tower != null;
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
