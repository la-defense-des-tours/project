using System.Collections.Generic;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Towers;
using Assets.Scripts.LaDefenseDesTours.Towers.Data;
using Assets.Scripts.LaDefenseDesTours.UI.HUD;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private TowerFactory machineGunFactory;
    [SerializeField] private TowerFactory laserFactory;
    [SerializeField] private TowerFactory canonFactory;

    [SerializeField] private GameObject machineGunGhostPrefab;
    [SerializeField] private GameObject laserGhostPrefab;
    [SerializeField] private GameObject canonGhostPrefab;

    private TowerFactory selectedFactory;
    private GameObject currentGhost;
    private bool isPlacingTower = false;
    private bool isGhostPlacementValid = false;

    private List<TowerSpawnButton> spawnButtons = new List<TowerSpawnButton>();
    private Cell selectedCell;
    public UpgradeMenu upgradeMenu;

    public static TowerManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple TowerManager instances detected!");
            return;
        }
        Instance = this;
    }

    public void SelectCell(Cell cell)
    {
        if (cell == selectedCell)
        {
            DeselectCell();
            return;
        }
        selectedCell = cell;
        upgradeMenu.SetTarget(cell);
    }

    public void DeselectCell()
    {
        selectedCell = null;
        upgradeMenu.Hide();
    }

    public void TryPlaceTowerOnCell(Cell cell)
    {
        if (!isPlacingTower || selectedFactory == null || cell.IsOccupied())
        {
            Debug.Log("Can't place tower here!");
            return;
        }

        Tower newTower = selectedFactory.CreateTower(cell.GetBuildPosition());
        if (newTower != null)
        {
            cell.SetTower(newTower, selectedFactory);
        }

        CancelGhostPlacement();
    }

    public void RegisterSpawnButton(TowerSpawnButton button)
    {
        if (button == null)
        {
            Debug.LogError("Trying to register a null button in TowerManager!");
            return;
        }

        spawnButtons.Add(button);
        button.buttonTapped += OnTowerButtonTapped;
        Debug.Log($"Button registered: {button.name}, total buttons: {spawnButtons.Count}");
    }
    private void OnTowerButtonTapped(TowerData towerData)
    {
        Debug.Log($"Tower button clicked: {towerData.towerName}");
        StartPlacingTower(towerData);


    }

    public void UpgradeTower(Cell cell)
    {
        if (cell.currentFactory == null)
        {
            Debug.LogError("No factory stored in this cell! Cannot upgrade.");
            return;
        }

        if (cell.tower == null)
        {
            Debug.LogError("No tower in this cell! Cannot upgrade.");
            return;
        }

        // Détruire l'ancienne tour
        Destroy(cell.tower.gameObject);

        // Créer la tour améliorée via la factory de la cellule
        Tower upgradedTower = cell.currentFactory.UpgradeTower(cell.GetBuildPosition(), cell.tower.currentLevel, cell.tower);

        if (upgradedTower != null)
        {
            cell.SetTower(upgradedTower, cell.currentFactory);
            cell.isUpgraded = true;

            upgradedTower.Upgrade();

            Debug.Log($"New Tower upgraded to level: {upgradedTower.currentLevel}");
            Debug.Log($"New Tower Name: {upgradedTower.towerName}");
            Debug.Log($"New Tower Damage: {upgradedTower.damage}");
            Debug.Log($"New Tower Range: {upgradedTower.range}");
            Debug.Log($"New Tower Cost: {upgradedTower.cost}");

            // Afficher les spécificités selon le type de tour
            if (upgradedTower is LaserTower laserTower)
            {
                Debug.Log($"New Tower Damage Over Time: {laserTower.damageOverTime}");
            }
            else if (upgradedTower is CanonTower canonTower)
            {
                Debug.Log($"New Area of Effect: {canonTower.areaOfEffect}");
            }
            else if (upgradedTower is MachineGunTower machineGunTower)
            {
                Debug.Log($"New Attack Per Second: {machineGunTower.attackPerSecond}");
            }
        }
        else
        {
            Debug.LogError("Upgrade failed!");
        }
    }

    // ================= GESTION DES GHOSTS ================= //

    public void StartPlacingTower(TowerData towerData)
    {
        if (currentGhost != null)
        {
            CancelGhostPlacement();
        }

        switch (towerData.towerName)
        {
            case "Machine Gun":
                selectedFactory = machineGunFactory;
                currentGhost = Instantiate(machineGunGhostPrefab);
                break;
            case "Laser":
                selectedFactory = laserFactory;
                currentGhost = Instantiate(laserGhostPrefab);
                break;
            case "Canon":
                selectedFactory = canonFactory;
                currentGhost = Instantiate(canonGhostPrefab);
                break;
            default:
                Debug.LogError("Invalid tower name");
                return;
        }

        if (currentGhost != null)
        {
            currentGhost.SetActive(true);
        }

        isPlacingTower = true;
        GameUI.instance.SetToBuildMode(towerData);
    }

    private void Update()
    {
        if (isPlacingTower && currentGhost != null)
        {
            MoveGhostToMouse();
        }

        if (Input.GetMouseButtonDown(1) && isPlacingTower)
        {
            CancelGhostPlacement();
        }
    }

    private void MoveGhostToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Cell cell = hit.collider.GetComponent<Cell>();

            if (cell != null && !cell.IsOccupied())
            {
                currentGhost.transform.position = cell.GetBuildPosition();
                isGhostPlacementValid = true;
            }
            else
            {
                isGhostPlacementValid = false;
            }
        }
        else
        {
            isGhostPlacementValid = false;
        }

        UpdateGhostVisual();
    }

    private void UpdateGhostVisual()
    {
        Renderer ghostRenderer = currentGhost?.GetComponent<Renderer>();
        if (ghostRenderer != null)
        {
            ghostRenderer.material.color = isGhostPlacementValid ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
        }
    }

    public void CancelGhostPlacement()
    {
        if (currentGhost != null)
        {
            Destroy(currentGhost);
        }
        currentGhost = null;
        selectedFactory = null;
        isPlacingTower = false;
        isGhostPlacementValid = false;
        GameUI.instance.CancelGhostPlacement();
    }
}
