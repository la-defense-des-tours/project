using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public interface Tower
    {
        public void SetupNavMeshAgent();
        // public void Move(Vector3 destination);
        public void Upgrade();
        public void Attack();
        public void SetPosition(Vector3 position);
    }
}