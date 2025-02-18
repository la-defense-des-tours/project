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
        public override Tower UpgradeTower(Vector3 position, int upgradeLevel)
        {
            if (upgradeLevel == 0 && machineGunTowerUpgrade == null || upgradeLevel == 1 && machineGunTowerUpgrade2 == null)
            {
                Debug.LogError("MachineGun Tower Upgrade prefab is not assigned!");
                return null; // Le retour null est nécessaire pour éviter une erreur de compilation
            }

            if (upgradeLevel == 0)
            {
                Debug.Log("MachineGun Tower 1 Upgraded");
                GameObject instance = Instantiate(machineGunTowerUpgrade.gameObject, position, Quaternion.identity);
                return instance.GetComponent<MachineGunTower>();
            }

            else if (upgradeLevel == 1)
            {
                Debug.Log("MachineGun Tower 2 Upgraded");
                GameObject instance = Instantiate(machineGunTowerUpgrade2.gameObject, position, Quaternion.identity);
                return instance.GetComponent<MachineGunTower>();
            }
            Debug.LogError("Max upgrade level reached!");
            return null;
        }
        public override void Notify()
        {
            Debug.Log("MachineGun Tower Created");
        }
    }
}