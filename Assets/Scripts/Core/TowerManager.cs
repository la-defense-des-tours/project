using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;


namespace Assets.Scripts.LaDefenseDesTours.Towers {
    public class TowerManager : MonoBehaviour
    {
        [SerializeField] private TowerFactory machineGunFactory;
        [SerializeField] private TowerFactory laserFactory;
        [SerializeField] private TowerFactory canonFactory;
        [SerializeField] private Transform target;
        private Tower machineGun;
        private Tower laser;
        private Tower canon;

        public void Start()
        {
            SpawnTower();
        }
        public void SpawnTower()
        {
            machineGun = machineGunFactory.CreateTower();
            laser = laserFactory.CreateTower();
            canon = canonFactory.CreateTower();

            // machineGun.Move(target.position);
            // laser.Move(target.position);
            // canon.Move(target.position);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Tower machineGunClone = machineGun.Clone();
                Tower laserClone = laser.Clone();
                Tower canonClone = canon.Clone();

                // machineGunClone.Move(target.position);
                // laserClone.Move(target.position);
                // canonClone.Move(target.position);
            }
        }
    }
}