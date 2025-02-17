using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Burned : State
    {
        private float damageMultiplier = 10f;
        private float duration = 5f;

        public override void ApplyEffect()
        {
            if (enemy == null)
                return;

            duration -= Time.deltaTime;

            if (duration > 0)
                enemy.TakeDamage(damageMultiplier * Time.deltaTime);
            else
            {
                OnStateExit();
            }
        }
    }
}