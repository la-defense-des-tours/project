using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Paralyzed : State
    {
        private float duration = 2f;
        private float timer = 0f;

        public override void ApplyEffect()
        {
            if (enemy == null) return;

            timer += Time.deltaTime;
            if (timer <= duration)
            {
                Debug.Log("Enemy is paralyzed and cannot move.");
            }
            else
            {
                Debug.Log("Paralyzed effect ended.");
            }
        }
    }
}