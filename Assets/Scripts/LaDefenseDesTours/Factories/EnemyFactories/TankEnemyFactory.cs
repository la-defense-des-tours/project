using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class TankEnemyFactory : MonoBehaviour, EnemyFactory
    {
        public Enemy CreateEnemy()
        {
            Notify();
            return new TankEnemy();
        }
        public void Notify()
        {
            Debug.Log("Flying enemy created!");
        }
    }
}