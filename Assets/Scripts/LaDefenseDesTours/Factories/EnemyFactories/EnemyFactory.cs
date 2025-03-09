using Assets.Scripts.LaDefenseDesTours.Interfaces;
using LaDefenseDesTours.Enemies;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class EnemyFactory : MonoBehaviour
    {
        public abstract Enemy CreateEnemy();
        public abstract void Notify();
    }
}