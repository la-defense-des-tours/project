



using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;
using Assets.Scripts.LaDefenseDesTours.Towers;
using Assets.Scripts.LaDefenseDesTours.Towers.Data;
using Assets.Scripts.LaDefenseDesTours.UI.HUD;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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
    public static TowerManager Instance;


    private NavMeshAgent tempAgent; // Agent temporaire unique

    private void Start()
    {
        CreateTemporaryAgent();
    }

    private void CreateTemporaryAgent()
    {
        tempAgent = new GameObject("TempNavMeshAgent").AddComponent<NavMeshAgent>();
        tempAgent.radius = 1.0f;
        tempAgent.height = 3.0f;
        tempAgent.speed = 3.5f;
        tempAgent.areaMask = 1 << NavMesh.GetAreaFromName("Walkable");
        tempAgent.gameObject.SetActive(false);
    }

    private bool TestPathWithTemporaryAgent(Vector3 start, Vector3 goal)
    {
        if (tempAgent == null) CreateTemporaryAgent();

        tempAgent.gameObject.SetActive(true);

        if (!tempAgent.Warp(start))
        {
            return false;
        }

        NavMeshPath path = new NavMeshPath();
        bool hasPath = tempAgent.CalculatePath(goal, path) && path.status == NavMeshPathStatus.PathComplete;

        tempAgent.gameObject.SetActive(false);

        return hasPath;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple TowerManager instances detected!");
            return;
        }
        Instance = this;
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
        if (!isPlacingTower || selectedFactory == null || cell.IsOccupied() || !isGhostPlacementValid)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (selectedTowerData == null)
        {
            return;
        }

        bool wasOccupied = cell.IsOccupied();
        cell.SetTemporaryBlock(true); 
        bool isBlocked = IsPathBlocked(cell);
        cell.SetTemporaryBlock(wasOccupied); 

        if (isBlocked)
        {
            return;
        }

        if (!LevelManager.instance.currency.TryPurchase(selectedTowerData.cost))
        {
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

    
    public void StartPlacingTower(TowerData towerData)
    {
        if (currentGhost != null)
        {
            CancelGhostPlacement();
        }

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

    private Vector3 GetClosestNavMeshPoint(Vector3 position, float searchRadius = 10f)
    {
        NavMeshHit hit;
        bool found = NavMesh.SamplePosition(position, out hit, searchRadius, NavMesh.AllAreas);

        if (found)
        {
            return hit.position; 
        }

        for (float radius = searchRadius; radius <= 30f; radius += 5f)
        {
            if (NavMesh.SamplePosition(position, out hit, radius, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        return Vector3.zero; 
    }



    public bool IsPathBlocked(Cell cell)
    {
        Vector3 start = GetClosestNavMeshPoint(new Vector3(0, 0, 0));
        Vector3 goal = GetClosestNavMeshPoint(LevelManager.instance.GetEnemyEndPoint());

        bool wasOccupied = cell.IsOccupied();
        cell.SetTemporaryBlock(true); 

        bool pathExists = TestPathWithTemporaryAgent(start, goal);

        cell.SetTemporaryBlock(wasOccupied);

        return !pathExists;
    }

    private IEnumerator MoveGhostToMouseCoroutine(Cell cell)
    {
        cacheCell = cell;
        isGhostPlacementValid = false;
        UpdateGhostVisual();

        bool wasOccupied = cell.IsOccupied();

        currentGhost.transform.position = cell.GetBuildPosition();

        NavMeshObstacle ghostObstacle = currentGhost.GetComponent<NavMeshObstacle>();
        if (ghostObstacle != null)
        {
            ghostObstacle.enabled = false;
            ghostObstacle.enabled = true;
            ghostObstacle.carving = true;

        }

        yield return WaitForNavMeshRecalculation();

        cell.SetTemporaryBlock(true);
        bool isBlocked = IsPathBlocked(cell);
        cell.SetTemporaryBlock(wasOccupied);

        if (ghostObstacle != null)
        {
            ghostObstacle.enabled = false;
        }

        bool isOccupied = wasOccupied;
        isGhostPlacementValid = !isOccupied && !isBlocked;

        UpdateGhostVisual();
    }

    private IEnumerator WaitForNavMeshRecalculation()
    {
        float timeout = 1.0f; // Temps max d'attente (éviter blocage infini)
        float timer = 0f;

        while (!NavMeshIsReady() && timer < timeout)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }

        if (timer >= timeout)
        {
            Debug.LogWarning("⏳ Attente NavMesh dépassée !");
        }
    }
    private bool NavMeshIsReady()
    {
        return !NavMesh.pathfindingIterationsPerFrame.Equals(0);
    }

    private void MoveGhostToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Cell cell = hit.collider.GetComponent<Cell>();

            if (cell == null)
            {
                return;
            }

            if (cell == cacheCell)
            {
                return;
            }

            StartCoroutine(MoveGhostToMouseCoroutine(cell));
        }
        else
        {
            isGhostPlacementValid = false;
            Debug.Log("[MoveGhostToMouse] ❌ Aucun objet détecté sous la souris.");
            UpdateGhostVisual();
        }
    }



    private void UpdateGhostVisual()
    {
        if (currentGhost == null) return;

        Renderer[] ghostRenderers = currentGhost.GetComponentsInChildren<Renderer>();

        Color validColor = new Color(0, 1, 0, 0.5f); 
        Color invalidColor = new Color(1, 0, 0, 0.5f); 

        foreach (Renderer renderer in ghostRenderers)
        {
            if (renderer.material.HasProperty("_Color")) 
            {
                renderer.material.color = isGhostPlacementValid ? validColor : invalidColor;
            }
        }
    }

    private void UpdateRangeIndicator(float range)
    {
        if (currentRangeIndicator != null)
        {
            DrawCircle drawCircle = currentRangeIndicator.GetComponent<DrawCircle>();
            if (drawCircle != null)
            {
                drawCircle.SetRadius(range / 2);
            }
        }
    }

    public void CancelGhostPlacement()
    {
        if (currentRangeIndicator != null)
        {
            currentRangeIndicator.SetActive(false);
        }
        if (currentGhost != null)
        {
            Destroy(currentGhost);
        }
        currentGhost = null;
        currentRangeIndicator = null;
        selectedFactory = null;
        isPlacingTower = false;
        isGhostPlacementValid = false;
        GameUI.instance.CancelGhostPlacement();
    }
}




