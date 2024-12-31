using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;


namespace Assets.Scripts.LaDefenseDesTours.Towers {
    public class TowerManager : MonoBehaviour
    {
        [SerializeField] private TowerFactory machineGunFactory;
        [SerializeField] private TowerFactory laserFactory;
        [SerializeField] private TowerFactory canonFactory;
        [SerializeField] private Transform target;
        [SerializeField] private LayerMask placementZoneLayer;
        private Tower machineGun;
        private Tower laser;
        private Tower canon;
        private TowerFactory selectedFactory;

        public void Start()
        {
            // Sélection par défaut
            selectedFactory = machineGunFactory;
        }
        public void Update()
        {
            HandleTowerSelection();

            if (Input.GetMouseButtonDown(0)) // Bouton gauche de la souris
            {
                TryPlaceTower();
            }
        }
        private void HandleTowerSelection()
        {
            // Touches G, L et C pour sélectionner les tours
            // Il faudra par la suite changer la sélection par des boutons dans l'UI qui seront liés à des événements monnaie
            if (Input.GetKeyDown(KeyCode.G))
            {
                selectedFactory = machineGunFactory;
                Debug.Log("Selected Tower: Machine Gun");
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                selectedFactory = laserFactory;
                Debug.Log("Selected Tower: Laser");
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                selectedFactory = canonFactory;
                Debug.Log("Selected Tower: Canon");
            }
        }
        private void TryPlaceTower()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Vérifie si le raycast tape sur une position valide
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementZoneLayer))
            {
                Vector3 placementPosition = hit.point;
                PlaceTower(placementPosition);
                Debug.Log("Good placement zone.");
            }
            else
            {
                Debug.Log("Invalid placement zone.");
            }
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