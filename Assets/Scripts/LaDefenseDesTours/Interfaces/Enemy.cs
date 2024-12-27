using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public interface Enemy
    {
        public void SetupNavMeshAgent();
        public void Move(Vector3 destination);
        public Enemy Clone();
        public void TakeDamage(float damage);
        public void Die();
    }
}