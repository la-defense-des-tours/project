using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserFactory : TowerFactory
    {
        [SerializeField] private LaserTower laserTower;
        public override GameObject CreateTower()
        {
            Notify();
            return laserTower.gameObject;
        }
        public override void Notify()
        {
            Debug.Log("Laser Tower prefab returned, not instantiated.");
        }
    }
}