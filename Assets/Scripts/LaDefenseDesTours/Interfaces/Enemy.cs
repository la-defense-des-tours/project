using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public interface Enemy
    {
        public void TransitionTo(State state);
        public void UpdateState();
        public void SetupNavMeshAgent();
        public void Move(Vector3 destination);
        public Enemy Clone(Transform spawnPoint);
        public void TakeDamage(float damage);
        public void DealDamage(double damage);
        public float GetSpeed();
        public void SetSpeed(float speed);
        public void Die();
    }
}