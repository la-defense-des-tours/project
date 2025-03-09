using Assets.Scripts.LaDefenseDesTours.Interfaces;
using LaDefenseDesTours.Enemies;
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
            
            Transform particleTransform = enemy.transform.Find(GetType().Name + "Effect");
            stateEffect = (particleTransform != null) ? particleTransform.GetComponentInChildren<ParticleSystem>() : null;
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
