using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Paralyzed : State
    {
        private float duration = 5f;

        public override void ApplyEffect()
        {
            if (enemy == null)
                return;

            duration -= Time.deltaTime;
            if (duration > 0)
                enemy.SetSpeed(0); 
            else
                enemy.SetupNavMeshAgent();
        }
    }
}