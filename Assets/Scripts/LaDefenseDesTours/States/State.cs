using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class State
    {
        protected Enemy enemy;

        public void SetContext(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public abstract void ApplyEffect();
    }
}