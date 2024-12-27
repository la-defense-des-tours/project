using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class WalkingEnemyFactory : EnemyFactory
    {
        [SerializeField] private WalkingEnemy walkingEnemy;
        public override Enemy CreateEnemy()
        {
            Notify();
            GameObject instance = Instantiate(walkingEnemy.gameObject);
            return instance.GetComponent<WalkingEnemy>();
        }
        public override void Notify()
        {
            Debug.Log("Walking enemy created!");
        }
    }
}