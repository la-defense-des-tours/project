using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Slowed : State
    {
        private float slowFactor = 0.5f;
        private float duration = 5f;
        private float originalSpeed;

        public override void ApplyEffect()
        {
            if (enemy == null)
                return;

            originalSpeed = enemy.GetSpeed();
            duration -= Time.deltaTime;

            if (duration > 0)
            {
                enemy.SetSpeed(originalSpeed * slowFactor);
                enemy.SetupNavMeshAgent();
            }
            else
            {
                enemy.SetSpeed(originalSpeed);
                enemy.SetupNavMeshAgent();
            }
        }
    }
}