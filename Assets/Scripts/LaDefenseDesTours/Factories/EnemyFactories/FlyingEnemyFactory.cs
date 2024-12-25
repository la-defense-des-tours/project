using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class FlyingEnemyFactory : EnemyFactory
    {
        public Enemy CreateEnemy()
        {
            Notify();
            return new FlyingEnemy();
        }
        public void Notify()
        {
            Debug.Log("Flying enemy created!");
        }
    }
}