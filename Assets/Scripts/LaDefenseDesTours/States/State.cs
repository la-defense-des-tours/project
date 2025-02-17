using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class State
    {
        protected Enemy enemy;
        protected ParticleSystem stateEffect;

        public void SetContext(Enemy enemy)
        {
            this.enemy = enemy;
            stateEffect = enemy.transform.Find(GetType().Name + "Effect").GetComponentInChildren<ParticleSystem>();
        }
        public virtual void OnStateEnter()
        {
            if (stateEffect != null)
                stateEffect.Play();
        }
        public virtual void OnStateExit()
        {
            if (stateEffect != null)
                stateEffect.Stop();
        }
        public abstract void ApplyEffect();
    }
}