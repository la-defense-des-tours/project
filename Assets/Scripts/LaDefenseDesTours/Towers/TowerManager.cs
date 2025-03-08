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
    private GameObject currentRangeIndicator;
    private TowerFactory selectedFactory;
    private GameObject currentGhost;
    private bool isPlacingTower = false;
    private bool isGhostPlacementValid = false;

    private readonly List<TowerSpawnButton> spawnButtons = new();
    private Cell cacheCell;
    private Tower selectedTower;
    private PlacementValidator placementValidator;
    public static TowerManager instance;
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

    private void Start()
    {
        if (GameUI.instance != null)
            GameUI.instance.stateChanged += OnGameStateChanged;
    }

    private void Update()
    {
        if (isPlacingTower && currentGhost != null)
            MoveGhostToMouse();
        if (Input.GetMouseButtonDown(1) && isPlacingTower)
            CancelGhostPlacement();
    }

    public void SelectCell(Tower tower)
    {


        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        GameUI.instance.towerUI.Show(tower);
    }

    public void TryPlaceTowerOnCell(Cell cell)
    {
        if (!isPlacingTower)
            return;
        if (selectedFactory == null)
            return;
        if (selectedTower == null)
            return;
        if (cell.IsOccupied())
            return;
        if (!isGhostPlacementValid)
            return;
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!LevelManager.instance.currency.TryPurchase(selectedTower.towerData.cost))
            return;

        Tower newTower = selectedFactory.CreateTower(cell.GetBuildPosition());
        if (newTower != null)
        {
            newTower.transform.SetParent(cell.transform);
        }
        CancelGhostPlacement();
    }

    public void RegisterSpawnButton(TowerSpawnButton button)
    {
        if (button == null)
        {
            return;
        }
        spawnButtons.Add(button);
        button.buttonTapped += OnTowerButtonTapped;
    }
    private void OnTowerButtonTapped(Tower tower)
    {
        StartPlacingTower(tower);


    }
    public void StartPlacingTower(Tower tower)
    {
        if (currentGhost != null)
            CancelGhostPlacement();

        selectedTower = tower;

        selectedFactory = selectedTower.towerData.factories;
        currentGhost = selectedTower.towerData.factories.CreateTower(Input.mousePosition, 1).gameObject;
        currentGhost.GetComponent<Tower>().isGhost = true;

        if (currentGhost != null)
        {
            currentGhost.SetActive(true);
            Transform rangeIndicator = currentGhost.transform.Find("rangeIndicator");
            if (rangeIndicator != null)
            {
                currentRangeIndicator = rangeIndicator.gameObject;
                currentRangeIndicator.SetActive(true);
                UpdateRangeIndicator(selectedTower.towerData.range);
            }
        }
        isPlacingTower = true;
        GameUI.instance.SetToBuildMode(tower);
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