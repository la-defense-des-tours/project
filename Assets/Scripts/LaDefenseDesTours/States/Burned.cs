using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Burned : State
    {
        private float damageOverTime = 5f;
        private float duration = 5f;

        public override void ApplyEffect()
        {
            if (enemy == null)
                return;

            duration -= Time.deltaTime;

            if (duration > 0)
            {
                enemy.TakeDamage(damageOverTime * Time.deltaTime);
            }
            else
            {
                Debug.Log("Burned effect ended.");
            }
        }
    }
}