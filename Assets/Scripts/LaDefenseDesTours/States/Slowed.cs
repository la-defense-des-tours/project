using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Slowed : State
    {
        private float slowFactor = 0.5f;
        private float duration = 4f;
        private float timer = 0f;
        private float originalSpeed;

        public override void ApplyEffect()
        {
            if (enemy == null)
                return;

            originalSpeed = enemy.GetSpeed();

            timer += Time.deltaTime;
            if (timer <= duration)
            {
                enemy.SetSpeed(originalSpeed * slowFactor);
            }
            else
            {
                enemy.SetSpeed(originalSpeed);
            }
        }
    }
}