using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class CanonFactory : TowerFactory
    {
        [SerializeField] private CanonTower canonTower;
        public override GameObject CreateTower()
        {
            Notify();
            return canonTower.gameObject;
        }
        public override void Notify()
        {
            Debug.Log("Canon Tower prefab returned, not instantiated.");
        }
    }
}