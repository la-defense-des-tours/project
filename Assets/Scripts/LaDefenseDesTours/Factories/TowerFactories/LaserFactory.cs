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

        public override void Notify()
        {
            Debug.Log("Laser Tower Created");
        }
    }
}