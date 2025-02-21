using System.Collections.Generic;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Towers.Data;
using Assets.Scripts.LaDefenseDesTours.UI.HUD;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private TowerFactory machineGunFactory;
    [SerializeField] private TowerFactory laserFactory;
    [SerializeField] private TowerFactory canonFactory;

    [SerializeField] private GameObject machineGunGhostPrefab;
    [SerializeField] private GameObject laserGhostPrefab;
    [SerializeField] private GameObject canonGhostPrefab;

    private GameObject currentGhost;
    private TowerFactory selectedFactory;
    public static TowerManager Instance;
    private bool isPlacingTower = false;
    private bool isGhostPlacementValid = false;

    [SerializeField] private LayerMask placementAreaMask;

    private List<TowerSpawnButton> spawnButtons = new List<TowerSpawnButton>();
    private Cell selectedCell;
    public UpgradeMenu upgradeMenu;

    public bool IsPlacingTower => isPlacingTower;
    public bool CanBuild() => selectedFactory != null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple TowerManager instances detected!");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        foreach (var button in spawnButtons)
        {
            button.buttonTapped += OnTowerButtonTapped;
        }
    }
    public TowerFactory GetSelectedFactory()
    {
        return selectedFactory;
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

    private void OnTowerButtonTapped(TowerData towerData)
    {
        Debug.Log($"Tower button clicked: {towerData.towerName}");
        StartPlacingTower(towerData);


    }

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

    private void MoveGhostToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementAreaMask))
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

    public void TryPlaceTowerOnCell(Cell cell)
    {
        if (!isGhostPlacementValid || selectedFactory == null || cell.IsOccupied())
        {
            Debug.Log("Can't place tower here!");
            return;
        }

        Tower newTower = selectedFactory.CreateTower(cell.GetBuildPosition());
        if (newTower != null)
        {
            cell.SetTower(newTower);
            cell.currentFactory = selectedFactory; 
        }

        CancelGhostPlacement();
    }

    public void CancelGhostPlacement()
    {
        if (currentGhost != null)
        {
            Destroy(currentGhost);
        }
        Debug.Log("Enter in cancel ghost");
        currentGhost = null;
        selectedFactory = null;
        isPlacingTower = false;  // <- AJOUTER CETTE LIGNE
        isGhostPlacementValid = false; // <- AJOUTER CETTE LIGNE
        GameUI.instance.CancelGhostPlacement();
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
}
