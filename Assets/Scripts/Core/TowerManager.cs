using System.Collections.Generic;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.LaDefenseDesTours.Towers {
    public class TowerManager : MonoBehaviour
    {
        [SerializeField] private TowerFactory machineGunFactory;
        [SerializeField] private TowerFactory laserFactory;
        [SerializeField] private TowerFactory canonFactory;
        [SerializeField] private Transform target;
        [SerializeField] private LayerMask placementZoneLayer;
        [SerializeField] private List<Grid> gridManagers;

        private Tower machineGun;
        private Tower laser;
        private Tower canon;
        [Header("UI Tower Selection Buttons")]
        [SerializeField] private Button machineGunButton;
        [SerializeField] private Button laserButton;
        [SerializeField] private Button canonButton;
        private TowerFactory selectedFactory;


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
            // HandleTowerSelection();

            if (Input.GetMouseButtonDown(1)) // Bouton droit de la souris
            {
                TryPlaceTower();
            }
        }
        // private void HandleTowerSelection()
        // {
        //     // Touches M, L et C pour sélectionner les tours
        //     // Il faudra par la suite changer la sélection par des boutons dans l'UI qui seront liés à des événements monnaie
        //     if (Input.GetKeyDown(KeyCode.M))
        //     {
        //         selectedFactory = machineGunFactory;
        //         Debug.Log("Selected Tower: Machine Gun");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.L))
        //     {
        //         selectedFactory = laserFactory;
        //         Debug.Log("Selected Tower: Laser");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.C))
        //     {
        //         selectedFactory = canonFactory;
        //         Debug.Log("Selected Tower: Canon");
        //     }
        // }

        private void SelectTower(TowerFactory factory, string towerName)
        {
            selectedFactory = factory;
            Debug.Log($"Selected Tower: {towerName}");
        }
        private void TryPlaceTower()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementZoneLayer))
            {
                Grid targetGrid = FindGridUnderHit(hit);
                Debug.Log($"Hit object: {hit.transform.position}");
                if (targetGrid != null)
                {
                    Vector3 gridPosition = hit.transform.position;
                Debug.Log($"Grid position: {gridPosition}");


                    if (targetGrid.IsCellAvailable(gridPosition))
                    {
                        PlaceTower(gridPosition);
                        targetGrid.OccupyCell(gridPosition);
                        Debug.Log($"Tower placed at grid position {gridPosition} on grid {targetGrid.name}");
                    }
                    else
                    {
                        Debug.Log("Invalid placement or cell already occupied.");
                    }
                }
                else
                {
                    Debug.Log("No valid grid under the mouse click.");
                }
            }
        }

        private Grid FindGridUnderHit(RaycastHit hit)
        {
            foreach (Grid gridManager in gridManagers)
            {
                if (hit.collider.transform.IsChildOf(gridManager.transform))
                {
                    return gridManager;
                }
            }
            return null;
        }

        private void PlaceTower(Vector3 position)
        {
            if (selectedFactory == null)
            {
                Debug.Log("No factory selected!");
                return;
            }

            Tower newTower = selectedFactory.CreateTower();
            newTower.SetPosition(position);
            Debug.Log($"Tower placed at: {position}");
        }
    }
}