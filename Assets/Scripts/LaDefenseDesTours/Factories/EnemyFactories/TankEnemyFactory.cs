using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class TankEnemyFactory : EnemyFactory
    {
        [SerializeField] private TankEnemy tankEnemy;

        public override Enemy CreateEnemy()
        {
            Notify();
            GameObject instance = Instantiate(tankEnemy.gameObject);
            return instance.GetComponent<TankEnemy>();
        }
        public override void Notify()
        {
            Debug.Log("Tank enemy created!");
        }
    }
}