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

        public override Tower UpgradeTower(Vector3 position, int upgradeLevel, Tower currentTower)
        {
            if ((upgradeLevel == 0 && laserTowerUpgrade == null) ||
                (upgradeLevel == 1 && laserTowerUpgrade2 == null))
            {
                Debug.LogError("Laser Tower Upgrade prefab is not assigned!");
                return null;
            }

            GameObject instance;
            LaserTower upgradedTower;

            // Utilisation d'un switch pour gérer les différents niveaux d'amélioration
            switch (upgradeLevel)
            {
                case 0:
                    Debug.Log("Laser Tower 1 Upgraded");
                    instance = Instantiate(laserTowerUpgrade.gameObject, position, Quaternion.identity);
                    upgradedTower = instance.GetComponent<LaserTower>();
                    break;

                case 1:
                    Debug.Log("Laser Tower 2 Upgraded");
                    instance = Instantiate(laserTowerUpgrade2.gameObject, position, Quaternion.identity);
                    upgradedTower = instance.GetComponent<LaserTower>();
                    break;

                default:
                    Debug.LogError("Max upgrade level reached!");
                    return null;
            }

            // Transfert des propriétés de la tour actuelle à la tour améliorée
            if (currentTower != null)
            {
                upgradedTower.currentLevel = currentTower.currentLevel;
                upgradedTower.damage = currentTower.damage;
                upgradedTower.range = currentTower.range;
                upgradedTower.cost = currentTower.cost;
                upgradedTower.damageOverTime = (currentTower as LaserTower).damageOverTime = 0f;
            }

            return upgradedTower;
        }

        public override void Notify()
        {
            Debug.Log("Laser Tower Created");
        }
    }
}