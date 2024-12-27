using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class FlyingEnemy : MonoBehaviour, Enemy
    {
        private NavMeshAgent agent;

        public void Awake()
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
            agent.speed = 6;
            agent.acceleration = 12;
        }
        public void Move(Vector3 destination)
        {
            agent.SetDestination(destination);
        }
        public Enemy Clone()
        {
            return (Enemy)MemberwiseClone();
        }
        public void TakeDamage(int damage)
        {
        }
        public void Die()
        {
            Destroy(gameObject);
        }
    }
}