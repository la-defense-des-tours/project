using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class MachineGunFactory : TowerFactory
    {
        [SerializeField] private MachineGunTower machineGunTower;
        [SerializeField] private MachineGunTower machineGunTowerUpgrade;
        [SerializeField] private MachineGunTower machineGunTowerUpgrade2;
        public override Tower CreateTower(Vector3 position)
        {
            Notify();
            GameObject instance = Instantiate(machineGunTower.gameObject, position, Quaternion.identity);
            return instance.GetComponent<MachineGunTower>();
        }
        public override Tower UpgradeTower(Vector3 position)
        {
            Notify();
            GameObject instance = Instantiate(machineGunTowerUpgrade.gameObject, position, Quaternion.identity);
            return instance.GetComponent<MachineGunTower>();
        }
        public override void Notify()
        {
            Debug.Log("MachineGun Tower Created");
        }
    }
}