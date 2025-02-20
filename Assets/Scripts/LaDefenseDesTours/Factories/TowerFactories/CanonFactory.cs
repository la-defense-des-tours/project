using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class CanonFactory : TowerFactory
    {
        [SerializeField] private CanonTower canonTower;
        [SerializeField] private CanonTower canonTowerUpgrade;
        [SerializeField] private CanonTower canonTowerUpgrade2;

        public override Tower CreateTower(Vector3 position)
        {
            Notify();
            GameObject instance = Instantiate(canonTower.gameObject, position, Quaternion.identity);
            return instance.GetComponent<CanonTower>();
        }

        public override Tower UpgradeTower(Vector3 position, int upgradeLevel, Tower currentTower)
        {
            if ((upgradeLevel == 1 && canonTowerUpgrade == null) ||
                (upgradeLevel == 2 && canonTowerUpgrade2 == null))
            {
                Debug.LogError("Canon Tower Upgrade prefab is not assigned!");
                return null;
            }

            GameObject instance;
            CanonTower upgradedTower;

            // Utilisation d'un switch pour gérer les différents niveaux d'amélioration
            switch (upgradeLevel)
            {
                case 1:
                    Debug.Log("Canon Tower has been upgraded to level 2");
                    instance = Instantiate(canonTowerUpgrade.gameObject, position, Quaternion.identity);
                    upgradedTower = instance.GetComponent<CanonTower>();
                    break;

                case 2:
                    Debug.Log("Canon Tower has been upgraded to level 3");
                    instance = Instantiate(canonTowerUpgrade2.gameObject, position, Quaternion.identity);
                    upgradedTower = instance.GetComponent<CanonTower>();
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
                upgradedTower.areaOfEffect = (currentTower as CanonTower).areaOfEffect = 0f;
            }

            return upgradedTower;
        }

        public override void Notify()
        {
            Debug.Log("Canon Tower Created");
        }
    }
}