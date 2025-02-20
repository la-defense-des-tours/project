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

        public override Tower UpgradeTower(Vector3 position, int upgradeLevel, Tower currentTower)
        {
            if ((upgradeLevel == 1 && machineGunTowerUpgrade == null) ||
                (upgradeLevel == 2 && machineGunTowerUpgrade2 == null))
            {
                Debug.LogError("MachineGun Tower Upgrade prefab is not assigned!");
                return null;
            }

            GameObject instance;
            MachineGunTower upgradedTower;

            // Utilisation d'un switch pour gérer les différents niveaux d'amélioration
            switch (upgradeLevel)
            {
                case 1:
                    Debug.Log("MachineGun Tower has been upgraded to level 2");
                    instance = Instantiate(machineGunTowerUpgrade.gameObject, position, Quaternion.identity);
                    upgradedTower = instance.GetComponent<MachineGunTower>();
                    break;

                case 2:
                    Debug.Log("MachineGun Tower has been upgraded to level 3");
                    instance = Instantiate(machineGunTowerUpgrade2.gameObject, position, Quaternion.identity);
                    upgradedTower = instance.GetComponent<MachineGunTower>();
                    break;

                default:
                    Debug.LogError("Max upgrade level reached!");
                    return null;
            }

            // Transfert des propriétés de la tour actuelle à la tour améliorée
            if (currentTower != null)
            {
                upgradedTower.currentLevel = currentTower.currentLevel;
                upgradedTower.damage = currentTower.damage;
                upgradedTower.range = currentTower.range;
                upgradedTower.cost = currentTower.cost;
                upgradedTower.attackPerSecond = (currentTower as MachineGunTower).attackPerSecond = 0f;
            }

            return upgradedTower;
        }

        public override void Notify()
        {
            Debug.Log("MachineGun Tower Created");
        }
    }
}