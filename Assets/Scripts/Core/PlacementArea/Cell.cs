using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Material hoverMaterial;
    public Vector3 positionOffset;
    private Renderer cellRenderer;
    private Material defaultMaterial;
    [HideInInspector]
    public Tower tower;
    [HideInInspector]
    public Tower towerUpgrade;
    public bool isUpgraded = false;
    private TowerManager towerManager;
    [HideInInspector]
    public TowerFactory currentFactory;
    public int upgradeLevel = 0;

    private void Start()
    {
        cellRenderer = GetComponent<Renderer>();
        defaultMaterial = cellRenderer.material;

        towerManager = TowerManager.Instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {

        if (tower != null)
        {
            towerManager.SelectCell(this);
            return;
        }

        if (!towerManager.canBuild)
        {
            Debug.Log("Not possible to build here!");
            return;
        }

        BuildTower(towerManager.GetSelectedFactory());
        if (tower == null)
        {
            Debug.Log("No tower selected!");
            return;
        }

        Debug.Log($"Tower placed at: {transform.position}");
    }

    public void UpgradeTower()
    {
        if (currentFactory == null)
        {
            Debug.Log("No tower selected!");
            return;
        }

        Destroy(this.tower.gameObject);
        Tower tower = currentFactory.UpgradeTower(GetBuildPosition(), upgradeLevel);

        if (tower != null)
        {
            this.tower = tower;
            isUpgraded = true;
            upgradeLevel++;
            Debug.Log("Tower upgraded to level: " + upgradeLevel);
        }
        else
        {
            Debug.LogError("Upgrade failed!");
        }
    }

    private void BuildTower(TowerFactory factory)
    {
        if (factory == null)
        {
            Debug.Log("No tower selected!");
            return;
        }
        Tower tower = factory.CreateTower(GetBuildPosition());
        this.tower = tower;
        currentFactory = factory;
        upgradeLevel = 0;
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

    public void SetTower(Tower newTower)
    {
        tower = newTower;
    }
}