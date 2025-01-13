using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Slowed : State
    {
        private float slowFactor = 0.5f;
        private float duration = 4f;
        private float timer = 0f;

        public override void ApplyEffect()
        {
            if (enemy == null) return;

            timer += Time.deltaTime;
            if (timer <= duration)
            {
                Debug.Log("Enemy is slowed.");
                enemy.SetupNavMeshAgent(); 
            }
            else
            {
                Debug.Log("Slowed effect ended.");
            }
        }
    }
}