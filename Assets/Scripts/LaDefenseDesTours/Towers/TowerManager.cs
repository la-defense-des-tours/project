



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
    public UpgradeMenu upgradeMenu;

    private TowerData selectedTowerData;
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Impossible de placer une tour sur l'interface utilisateur !");
            return;
        }
        if (selectedTowerData == null)
        {
            Debug.LogError("No TowerData selected! Cannot determine cost.");
            return;
        }

        if (IsPathBlocked(cell))
        {
            Debug.Log("Impossible de placer une tour ici, car elle bloque le passage des ennemis !");
            return;
        }

        if (!LevelManager.instance.currency.TryPurchase(selectedTowerData.cost))
        {
            Debug.Log("Pas assez d'argent !");
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

        Debug.Log($"[IsPathBlocked] Test de placement d'une tour sur {cell.gameObject.name}");

        bool wasOccupied = cell.IsOccupied();
        cell.SetTemporaryBlock(true);

        bool pathExists = TestPathWithTemporaryAgent(start, goal);

        cell.SetTemporaryBlock(wasOccupied);

        Debug.Log($"[IsPathBlocked] Résultat : {(pathExists ? "Chemin valide" : "Chemin bloqué")}");
        return !pathExists;
    }


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObj in enemies)
        {
            NavMeshAgent agent = enemyObj.GetComponent<NavMeshAgent>();
            if (agent == null || !agent.hasPath) continue;

            Gizmos.color = Color.cyan;
            for (int i = 0; i < agent.path.corners.Length - 1; i++)
            {
                Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
            }
        }
    }

    private bool TestPathWithTemporaryAgent(Vector3 start, Vector3 goal)
    {
        GameObject tempAgentObj = new GameObject("TempNavMeshAgent_Debug");
        NavMeshAgent tempAgent = tempAgentObj.AddComponent<NavMeshAgent>();

        tempAgent.radius = 1.0f;  
        tempAgent.height = 3.0f;
        tempAgent.speed = 3.5f;

        // ✅ Récupérer l'ID de la zone "Walkable"
        int walkableArea = NavMesh.GetAreaFromName("Walkable");
        if (walkableArea == -1)
        {
            Debug.LogError("[TestPathWithTemporaryAgent] ❌ L'aire 'Walkable' n'existe pas !");
            Destroy(tempAgentObj);
            return false;
        }

        tempAgent.areaMask = 1 << walkableArea; 

        NavMeshHit hitStart, hitGoal;
        bool startOnNavMesh = NavMesh.SamplePosition(start, out hitStart, 1.0f, tempAgent.areaMask);
        bool goalOnNavMesh = NavMesh.SamplePosition(goal, out hitGoal, 1.0f, tempAgent.areaMask);

        if (!startOnNavMesh)
        {
            Debug.LogError($"[TestPathWithTemporaryAgent] ❌ Le point de départ {start} n'est pas sur le NavMesh !");
            Destroy(tempAgentObj);
            return false;
        }

        if (!goalOnNavMesh)
        {
            Debug.LogError($"[TestPathWithTemporaryAgent] ❌ Le point d'arrivée {goal} n'est pas sur le NavMesh !");
            Destroy(tempAgentObj);
            return false;
        }

        // ✅ Déplacer l'agent au bon endroit
        if (!tempAgent.Warp(hitStart.position))
        {
            Debug.LogError("[TestPathWithTemporaryAgent] ❌ L'agent temporaire n'a pas pu être placé sur le NavMesh !");
            Destroy(tempAgentObj);
            return false;
        }

        NavMeshPath path = new NavMeshPath();
        bool hasPath = tempAgent.CalculatePath(hitGoal.position, path) && path.status == NavMeshPathStatus.PathComplete;

        Destroy(tempAgentObj);

        return hasPath;
    }





    private bool CanEnemyReachGoal(Enemy enemy, Vector3 goal)
    {
        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("[CanEnemyReachGoal] L'ennemi sélectionné n'a pas de NavMeshAgent !");
            return false;
        }

        Debug.Log($"[CanEnemyReachGoal] Test du chemin pour {enemy.name} vers {goal}");

        NavMeshPath path = new NavMeshPath();
        bool hasPath = agent.CalculatePath(goal, path);

        if (path.status != NavMeshPathStatus.PathComplete)
        {
            Debug.LogWarning($"[CanEnemyReachGoal] Path bloqué pour {enemy.name} ❌");
            return false;
        }

        Debug.Log($"[CanEnemyReachGoal] Chemin disponible pour {enemy.name} ✅");

        return true;
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

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Cell cell = hit.collider.GetComponent<Cell>();

            if (cell != null)
            {
                bool isBlocked = IsPathBlocked(cell);
                bool isOccupied = cell.IsOccupied();

                // ✅ Toujours déplacer le ghost pour qu'il suive la souris
                currentGhost.transform.position = cell.GetBuildPosition();

                if (!isOccupied && !isBlocked)
                {
                    isGhostPlacementValid = true;
                    Debug.Log($"[MoveGhostToMouse] ✅ Ghost placé à {currentGhost.transform.position}");
                }
                else
                {
                    isGhostPlacementValid = false;
                    Debug.Log($"[MoveGhostToMouse] ❌ Placement interdit - Occupé: {isOccupied}, Bloque chemin: {isBlocked}");
                }
            }
        }
        else
        {
            isGhostPlacementValid = false;
            Debug.Log("[MoveGhostToMouse] ❌ Aucun objet détecté sous la souris.");
        }

        UpdateGhostVisual();
    }



    private void UpdateGhostVisual()
    {
        if (currentGhost == null) return;

        Renderer[] ghostRenderers = currentGhost.GetComponentsInChildren<Renderer>();

        Color validColor = new Color(0, 1, 0, 0.5f); // ✅ Vert transparent
        Color invalidColor = new Color(1, 0, 0, 0.5f); // ❌ Rouge transparent

        foreach (Renderer renderer in ghostRenderers)
        {
            if (renderer.material.HasProperty("_Color")) // Vérifie si le matériel a une propriété couleur
            {
                renderer.material.color = isGhostPlacementValid ? validColor : invalidColor;
            }
        }

        Debug.Log($"[UpdateGhostVisual] Couleur du ghost mise à jour : {(isGhostPlacementValid ? "VERT ✅" : "ROUGE ❌")}");
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




