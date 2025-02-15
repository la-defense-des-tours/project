using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public interface Tower
    {
        public string towerName { get; }
        public float range { get; }
        public float damage { get; }
        public void SetupNavMeshAgent();
        public void Upgrade();
        public void Attack();
        public void SetPosition(Vector3 position);
    }
}