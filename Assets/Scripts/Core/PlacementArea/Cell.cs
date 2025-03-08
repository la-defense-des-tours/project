using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Towers;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Material hoverMaterial;
    public Vector3 positionOffset;
    private Renderer cellRenderer;
    private Material defaultMaterial;
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

        Tower detectedTower = FindTowerInCell(); 

        if (detectedTower != null)
        {
            towerManager.SelectCell(detectedTower);
            return;
        }
        towerManager.TryPlaceTowerOnCell(this);
    }
    public bool IsOccupied()
    {
        if (FindTowerInCell() != null)
        {
            return true;
        }
        return false;
    }

    private void OnMouseEnter()
    {
        cellRenderer.material = hoverMaterial;
        Tower detectedTower = FindTowerInCell();
        if (detectedTower != null)
        {
            detectedTower.ApplyHoverColor();
        }
    }

    private void OnMouseExit()
    {
        cellRenderer.material = defaultMaterial;

        Tower detectedTower = FindTowerInCell();
        if (detectedTower != null)
        {
            detectedTower.ResetColor();
        }
    }
}

