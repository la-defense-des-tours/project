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
            // Sélectionner la cellule si une tour est déjà dessus
            towerManager.SelectCell(this);
            return;
        }

        if (!towerManager.CanBuild() || towerManager.IsPlacingTower == false)
        {
            Debug.Log("Not possible to build here!");
            return;
        }

        // Placer la tour via TowerManager
        towerManager.TryPlaceTowerOnCell(this);
    }

    public void UpgradeTower()
    {
        if (currentFactory == null)
        {
            Debug.LogError("No factory stored in this cell! Cannot upgrade.");
            return;
        }

        if (tower == null)
        {
            Debug.LogError("No tower in this cell! Cannot upgrade.");
            return;
        }

        // Détruire l'ancienne tour
        Destroy(tower.gameObject);

        // Créer la tour améliorée
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

            // Afficher les spécificités selon le type de tour
            if (tower is LaserTower laserTower)
            {
                Debug.Log($"New Tower Damage Over Time: {laserTower.damageOverTime}");
            }
            else if (tower is CanonTower canonTower)
            {
                Debug.Log($"New Area of Effect: {canonTower.areaOfEffect}");
            }
            else if (tower is MachineGunTower machineGunTower)
            {
                Debug.Log($"New Attack Per Second: {machineGunTower.attackPerSecond}");
            }
        }
        else
        {
            Debug.LogError("Upgrade failed!");
        }
    }


    public bool IsOccupied()
    {
        return tower != null;
    }

    public void SetTower(Tower newTower)
    {
        tower = newTower;
        currentFactory = TowerManager.Instance.GetSelectedFactory();
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
