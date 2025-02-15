using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class MachineGunFactory : TowerFactory
    {
        [SerializeField] private MachineGunTower machineGunTower;
        public override GameObject CreateTower()
        {
            Notify();
            return machineGunTower.gameObject;
        }
        public override void Notify()
        {
            Debug.Log("Machine Gun Tower prefab returned, not instantiated.");
        }
    }
}
