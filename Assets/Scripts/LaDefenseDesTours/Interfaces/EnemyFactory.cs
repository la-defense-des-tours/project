using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces {

    public interface EnemyFactory
    {
        public abstract Enemy CreateEnemy();
        public abstract void Notify();
    }
}