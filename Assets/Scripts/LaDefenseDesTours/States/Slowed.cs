
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Slowed : State
    {
        private float duration = 5f;
        private float slowFactor = 0.5f;
        private float currentSpeed;
        private bool isApplied = false;
        public override void ApplyEffect()
        {
            if (enemy == null)
                return;

            if (!isApplied)
            {
                currentSpeed = enemy.GetSpeed();
                enemy.SetSpeed(currentSpeed * slowFactor);
                isApplied = true;
            }

            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                enemy.SetupNavMeshAgent();
                isApplied = false;
                OnStateExit();
            }
        }
    }
}