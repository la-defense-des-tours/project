using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Enemies;

namespace Assets.Scripts.LaDefenseDesTours.Factories
{
    public class BossEnemyFactory : EnemyFactory
    {
        [SerializeField] private BossEnemy bossEnemy;
        public override Enemy CreateEnemy()
        {
            Notify();
            GameObject instance = Instantiate(bossEnemy.gameObject, transform.position, Quaternion.identity);
            return instance.GetComponent<BossEnemy>();
        }
        public override void Notify()
        {
            Debug.Log("Boss enemy created!");
        }
    }
}
