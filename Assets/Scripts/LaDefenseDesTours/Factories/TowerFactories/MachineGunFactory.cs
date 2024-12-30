using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class MachineGunFactory : TowerFactory
    {
        [SerializeField] private MachineGunTower machineGunTower;
        public override Tower CreateTower()
        {
            Notify();
            GameObject instance = Instantiate(machineGunTower.gameObject);
            return instance.GetComponent<MachineGunTower>();
        }
        public override void Notify()
        {
            Debug.Log("Machine Gun Tower Created");
        }
    }
}
