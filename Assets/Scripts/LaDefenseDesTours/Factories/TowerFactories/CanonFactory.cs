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
        public override Tower UpgradeTower(Vector3 position, int upgradeLevel)
        {
            if (upgradeLevel == 0 && canonTowerUpgrade == null || upgradeLevel == 1 && canonTowerUpgrade2 == null)
            {
                Debug.LogError("Canon Tower Upgrade prefab is not assigned!");
                return null; // Le retour null est nécessaire pour éviter une erreur de compilation
            }

            if (upgradeLevel == 0)
            {
                Debug.Log("Canon Tower 1 Upgraded");
                GameObject instance = Instantiate(canonTowerUpgrade.gameObject, position, Quaternion.identity);
                return instance.GetComponent<CanonTower>();
            }

            else if (upgradeLevel == 1)
            {
                Debug.Log("Canon Tower 2 Upgraded");
                GameObject instance = Instantiate(canonTowerUpgrade2.gameObject, position, Quaternion.identity);
                return instance.GetComponent<CanonTower>();
            }
            Debug.LogError("Max upgrade level reached!");
            return null;
        }
        public override void Notify()
        {
            Debug.Log("Canon Tower Created");
        }
    }
}