using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Enemies;

namespace Assets.Scripts.LaDefenseDesTours.Factories
{
    public class WalkingEnemyFactory : EnemyFactory
    {
        [SerializeField] private WalkingEnemy walkingEnemy;
        public override Enemy CreateEnemy()
        {
            Notify();
            GameObject instance = Instantiate(walkingEnemy.gameObject, transform.position, Quaternion.Euler(0, -90, 0));
            return instance.GetComponent<WalkingEnemy>();
        }
        public override void Notify()
        {
            Debug.Log("Walking enemy created!");
        }
    }
}