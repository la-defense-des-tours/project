using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Towers;
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
        if (currentFactory == null || tower == null)
        {
            Debug.Log("No tower selected!");
            return;
        }

        Destroy(tower.gameObject);
        Tower upgradedTower = currentFactory.UpgradeTower(GetBuildPosition(), tower.currentLevel, tower);

        if (upgradedTower != null)
        {
            tower = upgradedTower;
            isUpgraded = true;

            tower.Upgrade();

            Debug.Log($"New Tower upgraded to level: {tower.currentLevel}");
            Debug.Log($"New Tower Name: {tower.towerName}");
            Debug.Log($"New Tower Damage: {tower.damage}");
            Debug.Log($"New Tower Range: {tower.range}");
            Debug.Log($"New Tower Cost: {tower.cost}");
            if (tower as LaserTower != null)
            {
                Debug.Log($"New Tower Damage Over Time: {(tower as LaserTower).damageOverTime}");
            }
            else if (tower as CanonTower != null)
            {
                Debug.Log($"New Area of Effect: {(tower as CanonTower).areaOfEffect}");
            }
            else if (tower as MachineGunTower != null)
            {
                Debug.Log($"New Attack Per Second: {(tower as MachineGunTower).attackPerSecond}");
            }
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

        tower.currentLevel = 1;

        Debug.Log($"Tower Name: {tower.towerName}");
        Debug.Log($"Tower Damage: {tower.damage}");
        Debug.Log($"Tower Range: {tower.range}");
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