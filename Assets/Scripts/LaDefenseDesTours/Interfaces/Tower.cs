using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public interface Tower
    {
        public void SetupNavMeshAgent();
        // public void Move(Vector3 destination);
        public Tower Clone();
        public void Upgrade();
        public void Attack();
    }
}