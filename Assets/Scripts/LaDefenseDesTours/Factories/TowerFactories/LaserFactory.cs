using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserFactory : TowerFactory
    {
        [SerializeField] private LaserTower laserTower;
        [SerializeField] private LaserTower laserTowerUpgrade;
        [SerializeField] private LaserTower laserTowerUpgrade2;

        public override Tower CreateTower(Vector3 position, int level=1)
        {
            Notify();
            GameObject prefabs;
            switch (level)
            {
                case 1:
                    prefabs = laserTower.gameObject;
                    break;
                case 2:
                    prefabs = laserTowerUpgrade.gameObject;
                    break;
                case 3:
                    prefabs = laserTowerUpgrade2.gameObject;
                    break;
                default:
                    return null;
            }
            GameObject instance = Instantiate(prefabs, position, Quaternion.identity);
            return instance.GetComponent<LaserTower>();
        }

        public override void Notify()
        {
            Debug.Log("Laser Tower Created");
        }
    }
}