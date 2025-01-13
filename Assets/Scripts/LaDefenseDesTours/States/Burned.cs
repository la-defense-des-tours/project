using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Burned : State
    {
        private float damageOverTime = 5f;
        private float duration = 3f;
        private float timer = 0f;

        public override void ApplyEffect()
        {
            if (enemy == null) return;
            timer += Time.deltaTime;
            if (timer <= duration)
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