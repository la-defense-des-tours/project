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

        public override Tower CreateTower(Vector3 position, int level = 1)
        {
            Notify();
            GameObject prefabs;
            switch (level)
            {
                case 1:
                    prefabs = machineGunTower.gameObject;
                    break;
                case 2:
                    prefabs = machineGunTowerUpgrade.gameObject;
                    break;
                case 3:
                    prefabs = machineGunTowerUpgrade2.gameObject;
                    break;
                default:
                    return null;
            }
            GameObject instance = Instantiate(prefabs, position, Quaternion.identity);
            return instance.GetComponent<MachineGunTower>();
        }
        public override void Notify()
        {
            Debug.Log("MachineGun Tower Created");
        }
    }
}