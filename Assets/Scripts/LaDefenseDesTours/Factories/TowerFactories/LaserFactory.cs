using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserFactory : TowerFactory
    {
        [SerializeField] private LaserTower laserTower;
        [SerializeField] private LaserTower laserTowerUpgrade;
        [SerializeField] private LaserTower laserTowerUpgrade2;
        public override Tower CreateTower(Vector3 position)
        {
            Notify();
            GameObject instance = Instantiate(laserTower.gameObject, position, Quaternion.identity);
            return instance.GetComponent<LaserTower>();
        }
        public override Tower UpgradeTower(Vector3 position, int upgradeLevel)
        {
            if (upgradeLevel == 0 && laserTowerUpgrade == null || upgradeLevel == 1 && laserTowerUpgrade2 == null)
            {
                Debug.LogError("Laser Tower Upgrade prefab is not assigned!");
                return null; // Le retour null est nécessaire pour éviter une erreur de compilation
            }

            if (upgradeLevel == 0)
            {
                Debug.Log("Laser Tower 1 Upgraded");
                GameObject instance = Instantiate(laserTowerUpgrade.gameObject, position, Quaternion.identity);
                return instance.GetComponent<LaserTower>();
            }
            
            else if (upgradeLevel == 1)
            {
                Debug.Log("Laser Tower 2 Upgraded");
                GameObject instance = Instantiate(laserTowerUpgrade2.gameObject, position, Quaternion.identity);
                return instance.GetComponent<LaserTower>();
            }
            Debug.LogError("Max upgrade level reached!");
            return null;
        }
        public override void Notify()
        {
            Debug.Log("Laser Tower Created");
        }
    }
}