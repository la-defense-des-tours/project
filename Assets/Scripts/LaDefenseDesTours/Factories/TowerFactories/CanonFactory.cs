using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class CanonFactory : TowerFactory
    {
        [SerializeField] private CanonTower canonTower;
        public override Tower CreateTower()
        {
            Notify();
            GameObject instance = Instantiate(canonTower.gameObject);
            return instance.GetComponent<CanonTower>();
        }
        public override void Notify()
        {
            Debug.Log("Canon Tower Created");
        }
    }
}