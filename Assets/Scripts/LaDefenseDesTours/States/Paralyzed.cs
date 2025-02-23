using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Paralyzed : State
    {
        private float duration = 5f;
        public bool isApplied = false;

        public override void ApplyEffect()
        {
           if (!isApplied)
            {
                enemy.SetSpeed(0);
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