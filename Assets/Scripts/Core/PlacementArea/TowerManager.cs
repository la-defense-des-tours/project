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
    public void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Bouton droit de la souris
        {
            TryPlaceTower();
        }
    }

    private void SelectTower(TowerFactory factory, string towerName)
    {
        selectedFactory = factory;
        Debug.Log($"Selected Tower: {towerName}");
    }
    private void TryPlaceTower()
    {
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, placementZoneLayer))
            return;

        Grid grid = FindGridUnderHit(hit);
        if (grid == null || !grid.IsCellAvailable(hit.point)) return;

        PlaceTower(hit.point);
        grid.OccupyCell(hit.point);
    }

    private Grid FindGridUnderHit(RaycastHit hit)
    {
        return grids.Find(grid => hit.collider.transform.IsChildOf(grid.transform));
    }

    private void PlaceTower(Vector3 position)
    {
        if (selectedFactory == null)
        {
            Debug.Log("No factory selected!");
            return;
        }

        Tower newTower = selectedFactory.CreateTower().GetComponent<Tower>();
        newTower.SetPosition(position);
        Debug.Log($"Tower placed at: {position}");
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