using System.Collections.Generic;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Towers.Data;
using Assets.Scripts.LaDefenseDesTours.UI.HUD;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private TowerFactory machineGunFactory;
    [SerializeField] private TowerFactory laserFactory;
    [SerializeField] private TowerFactory canonFactory;
    [SerializeField] private Transform target;
    private TowerFactory selectedFactory;
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

    public void Start()
    {
        // Sélection par défaut
        TowerSpawnButton[] spawnButtons = FindObjectsByType<TowerSpawnButton>(FindObjectsSortMode.None);
        Debug.Log($"Found {spawnButtons.Length} buttons");
        foreach (TowerSpawnButton button in spawnButtons)
        {
            button.buttonTapped += OnTowerButtonTapped;


        }
    }

    private void OnTowerButtonTapped(TowerData towerData)
    {
        SelectTower(towerData.towerName);
        Debug.Log("Tower selected");
    }

    private void SelectTower(string towerName)
    {
        switch (towerName)
        {
            case "Machine Gun":
                selectedFactory = machineGunFactory;
                break;
            case "Laser":
                selectedFactory = laserFactory;
                break;
            case "Canon":
                selectedFactory = canonFactory;
                break;
            default:
                Debug.LogError("Invalid tower name");
                break;
        }
        Debug.Log($"Selected Tower: {towerName}");
    }

    public Tower GetTowerToPlace(Vector3 position)
    {
        if (selectedFactory == null)
        {
            return null;
        }
        return selectedFactory.CreateTower(position);
    }
}