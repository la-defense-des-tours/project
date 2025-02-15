using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class CanonFactory : TowerFactory
    {
        [SerializeField] private CanonTower canonTower;
        public override Tower CreateTower(Vector3 position)
        {
            Notify();
            GameObject instance = Instantiate(canonTower.gameObject, position, Quaternion.identity);
            return instance.GetComponent<CanonTower>();
        }
        public override void Notify()
        {
            Debug.Log("Canon Tower Created");
        }
    }
}