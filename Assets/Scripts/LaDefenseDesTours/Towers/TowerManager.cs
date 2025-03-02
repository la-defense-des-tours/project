using System.Collections.Generic;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;
using Assets.Scripts.LaDefenseDesTours.Towers.Data;
using Assets.Scripts.LaDefenseDesTours.UI.HUD;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private TowerFactory machineGunFactory;
    [SerializeField] private TowerFactory laserFactory;
    [SerializeField] private TowerFactory canonFactory;
    [SerializeField] private GameObject machineGunGhostPrefab;
    [SerializeField] private GameObject laserGhostPrefab;
    [SerializeField] private GameObject canonGhostPrefab;

    private GameObject currentRangeIndicator;
    private TowerFactory selectedFactory;
    private GameObject currentGhost;
    private bool isPlacingTower = false;
    private bool isGhostPlacementValid = false;

    private readonly List<TowerSpawnButton> spawnButtons = new();
    private Cell selectedCell;
    private Cell cacheCell;
    public UpgradeMenu upgradeMenu;
    private TowerData selectedTowerData;
    public static TowerManager instance { get; private set; }
    private PlacementValidator placementValidator;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Multiple TowerManager instances detected!");
            return;
        }
        instance = this;
        placementValidator = GetComponent<PlacementValidator>();
    }

    private bool HasPath(Vector3 start, Vector3 goal)
    {
        NavMeshPath path = new NavMeshPath();
        bool hasPath = NavMesh.CalculatePath(start, goal, NavMesh.AllAreas, path) && path.status == NavMeshPathStatus.PathComplete;
        return hasPath;
    }

    private void Update()
    {
        if (isPlacingTower && currentGhost != null)
            MoveGhostToMouse();
        if (Input.GetMouseButtonDown(1) && isPlacingTower)
            CancelGhostPlacement();
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

    private void DeselectCell()
    {
        selectedCell = null;
        upgradeMenu.Hide();
    }

    public void TryPlaceTowerOnCell(Cell cell)
    {
        if (!isPlacingTower)
            return;
        if (selectedFactory == null)
            return;
        if (selectedTowerData == null)
            return;
        if (cell.IsOccupied())
            return;
        if (!isGhostPlacementValid)
            return;
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!LevelManager.instance.currency.TryPurchase(selectedTowerData.cost))
            return;

        Tower newTower = selectedFactory.CreateTower(cell.GetBuildPosition());
        if (newTower != null)
            cell.SetTower(newTower, selectedFactory);

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
    }

    private void OnTowerButtonTapped(TowerData towerData)
    {
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
        Destroy(cell.tower.gameObject);
        Tower upgradedTower = cell.currentFactory.UpgradeTower(cell.GetBuildPosition(), cell.tower.currentLevel, cell.tower);
        if (upgradedTower != null)
        {
            cell.SetTower(upgradedTower, cell.currentFactory);
            cell.isUpgraded = true;
            upgradedTower.Upgrade();
        }
        else
        {
            Debug.LogError("Upgrade failed!");
        }
    }

    private void StartPlacingTower(TowerData towerData)
    {
        if (currentGhost != null)
            CancelGhostPlacement();
        selectedTowerData = towerData;
        switch (towerData.towerName)
        {
            case "Machine Gun":
                selectedFactory = machineGunFactory;
                currentGhost = Instantiate(machineGunGhostPrefab, Input.mousePosition, Quaternion.identity);
                currentGhost.GetComponent<Tower>().isGhost = true;
                break;
            case "Laser":
                selectedFactory = laserFactory;
                currentGhost = Instantiate(laserGhostPrefab, Input.mousePosition, Quaternion.identity);
                currentGhost.GetComponent<Tower>().isGhost = true;
                break;
            case "Canon":
                selectedFactory = canonFactory;
                currentGhost = Instantiate(canonGhostPrefab, Input.mousePosition, Quaternion.identity);
                currentGhost.GetComponent<Tower>().isGhost = true;
                break;
            default:
                Debug.LogError("Invalid tower name");
                return;
        }
        if (currentGhost != null)
        {
            currentGhost.SetActive(true);
            Transform rangeIndicator = currentGhost.transform.Find("rangeIndicator");
            if (rangeIndicator != null)
            {
                currentRangeIndicator = rangeIndicator.gameObject;
                currentRangeIndicator.SetActive(true);
                UpdateRangeIndicator(selectedTowerData.range);
            }
        }
        isPlacingTower = true;
        GameUI.instance.SetToBuildMode(towerData);
    }

    private void MoveGhostToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Cell cell = hit.collider.GetComponent<Cell>();
            if (cell == null)
                return;
            if (cell == cacheCell)
                return;
            cacheCell = cell;
            
            StartCoroutine(placementValidator.ValidatePlacement(cell, currentGhost, (isValid) =>
            {
                isGhostPlacementValid = isValid;
                placementValidator.UpdateGhostVisual(currentGhost, isValid);
            }));
        }
        else
        {
            isGhostPlacementValid = false;
            Debug.Log("[MoveGhostToMouse] No cell detected.");
            placementValidator.UpdateGhostVisual(currentGhost, false);
        }
    }

    private void UpdateRangeIndicator(float range)
    {
        if (currentRangeIndicator != null)
        {
            DrawCircle drawCircle = currentRangeIndicator.GetComponent<DrawCircle>();
            drawCircle?.SetRadius(range / 2);
        }
    }

    private void OnEnable()
    {
        if (GameUI.instance != null)
            GameUI.instance.stateChanged += OnGameStateChanged;
    }

    private void OnDisable()
    {
        if (GameUI.instance != null)
            GameUI.instance.stateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameUI.State oldState, GameUI.State newState)
    {
        if (newState == GameUI.State.Paused || newState == GameUI.State.GameOver)
        {
            Debug.Log("[TowerManager] Canceling ghost due to game state change.");
            CancelGhostPlacement();
        }
    }

    public void CancelGhostPlacement()
    {
        currentRangeIndicator?.SetActive(false);
        if (currentGhost != null)
            Destroy(currentGhost);
        currentGhost = null;
        currentRangeIndicator = null;
        selectedFactory = null;
        isPlacingTower = false;
        isGhostPlacementValid = false;
        GameUI.instance.CancelGhostPlacement();
    }
}
