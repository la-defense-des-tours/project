using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public interface Enemy
    {
        public void Move(Vector3 destination);
        public Enemy Clone();
        public void TakeDamage(int damage);
        public void Die();
    }
}