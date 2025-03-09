using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using LaDefenseDesTours.Enemies;

namespace Assets.Scripts.LaDefenseDesTours.Factories
{
    public class TankEnemyFactory : EnemyFactory
    {
        [SerializeField] private TankEnemy tankEnemy;

        public override Enemy CreateEnemy()
        {
            Notify();
            GameObject instance = Instantiate(tankEnemy.gameObject, transform.position, Quaternion.Euler(0, -90, 0));
            return instance.GetComponent<TankEnemy>();
        }
        public override void Notify()
        {
            Debug.Log("Tank enemy created!");
        }
    }
}