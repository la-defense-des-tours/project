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
    [HideInInspector] public TowerFactory currentFactory;
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

    private void OnMouseDown()
    {
        if (tower != null)
        {
            towerManager.SelectCell(this, tower);
            return;
        }
        towerManager.TryPlaceTowerOnCell(this);
    }

    public void SetTower(Tower newTower, TowerFactory factory)
    {
        tower = newTower;
        currentFactory = factory;
    }

    public bool IsOccupied()
    {
        return tower != null;
    }

    public void RequestUpgrade()
    {
        towerManager.UpgradeTower(this);
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
