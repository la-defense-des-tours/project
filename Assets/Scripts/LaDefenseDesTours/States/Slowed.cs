using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Slowed : State
    {
        private float slowFactor = 0.5f;
        private float duration = 5f;
        private float originalSpeed;
        private bool isApplied = false;
        public override void ApplyEffect()
        {
            if (enemy == null)
                return;

            if (!isApplied)
            {
                originalSpeed = enemy.GetSpeed();
                enemy.SetSpeed(originalSpeed * slowFactor);
                enemy.SetupNavMeshAgent();
                isApplied = true;
            }

            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                enemy.SetSpeed(originalSpeed);
                enemy.SetupNavMeshAgent();
                isApplied = false;
            }
        }
    }
}