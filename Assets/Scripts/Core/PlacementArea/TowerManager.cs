using System.Collections.Generic;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private TowerFactory machineGunFactory;
    [SerializeField] private TowerFactory laserFactory;
    [SerializeField] private TowerFactory canonFactory;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask placementZoneLayer;
    [SerializeField] private List<Grid> grids;
    [SerializeField] private Button machineGunButton;
    [SerializeField] private Button laserButton;
    [SerializeField] private Button canonButton;
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
        selectedFactory = machineGunFactory;
        machineGunButton.onClick.AddListener(() => SelectTower(machineGunFactory, "Machine Gun"));
        laserButton.onClick.AddListener(() => SelectTower(laserFactory, "Laser"));
        canonButton.onClick.AddListener(() => SelectTower(canonFactory, "Canon"));
    }

    private void SelectTower(TowerFactory factory, string towerName)
    {
        selectedFactory = factory;
        Debug.Log($"Selected Tower: {towerName}");
    }

    public GameObject GetTowerToPlace()
    {
        if (selectedFactory == null)
        {
            return null;
        }
        return selectedFactory.CreateTower();
    }
}