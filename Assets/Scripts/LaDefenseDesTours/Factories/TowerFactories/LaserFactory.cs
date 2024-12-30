using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserFactory : TowerFactory
    {
        [SerializeField] private LaserTower laserTower;
        public override Tower CreateTower()
        {
            Notify();
            GameObject instance = Instantiate(laserTower.gameObject);
            return instance.GetComponent<LaserTower>();
        }
        public override void Notify()
        {
            Debug.Log("Laser Tower Created");
        }
    }
}