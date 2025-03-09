using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class CanonFactory : TowerFactory
    {
        [SerializeField] private CanonTower canonTower;
        [SerializeField] private CanonTower canonTowerUpgrade;
        [SerializeField] private CanonTower canonTowerUpgrade2;

        public override Tower CreateTower(Vector3 position, int level=1)
        {
            Notify();
            GameObject prefabs;
            switch (level)
            {
                case 1:
                    prefabs = canonTower.gameObject;
                    break;
                case 2:
                    prefabs = canonTowerUpgrade.gameObject;
                    break;
                case 3:
                    prefabs = canonTowerUpgrade2.gameObject;
                    break;
                default:
                    return null;
            }
            GameObject instance = Instantiate(prefabs, position, Quaternion.identity);
            return instance.GetComponent<CanonTower>();
        }
        public override void Notify()
        {
            Debug.Log("Canon Tower Created");
        }
    }
}