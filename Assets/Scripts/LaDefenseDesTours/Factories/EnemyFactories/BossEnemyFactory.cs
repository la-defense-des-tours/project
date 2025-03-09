using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using LaDefenseDesTours.Enemies;

namespace Assets.Scripts.LaDefenseDesTours.Factories
{
    public class BossEnemyFactory : EnemyFactory
    {
        [SerializeField] private BossEnemy bossEnemy;
        public override Enemy CreateEnemy()
        {
            Notify();
            GameObject instance = Instantiate(bossEnemy.gameObject, transform.position, Quaternion.Euler(0, -90, 0));
            return instance.GetComponent<BossEnemy>();
        }
        public override void Notify()
        {
            Debug.Log("Boss enemy created!");
        }
    }
}
