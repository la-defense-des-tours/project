using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class FlyingEnemy : MonoBehaviour, Enemy
    {
        private State currentState;
        private NavMeshAgent agent;
        private float health = 100;
        private float speed = 4;
        private float acceleration = 8;

        public void Awake()
        {
            SetupNavMeshAgent();
        }
        public void SetupNavMeshAgent()
        {
            if (gameObject.GetComponent<NavMeshAgent>() == null)
            {
                agent = gameObject.AddComponent<NavMeshAgent>();
            }
            else
            {
                agent = gameObject.GetComponent<NavMeshAgent>();
            }
            agent.speed = speed;
            agent.acceleration = acceleration;
        }
        public void Move(Vector3 destination)
        {
            if (currentState is not Paralyzed)
            {
                agent.SetDestination(destination);
            }
        }
        public Enemy Clone(Transform spawnPoint)
        {
            Enemy clone = Instantiate(this, spawnPoint.position, Quaternion.identity);
            clone.SetupNavMeshAgent();
            return clone;
        }
        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                TransitionTo(new Dead());
            }
        }
        public void Die()
        {
            Destroy(gameObject);
        }
        public void TransitionTo(State state)
        {
            currentState = state;
            currentState.SetContext(this);
            currentState.ApplyEffect();
        }
        public float GetSpeed()
        {
            return speed;
        }
        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }
    }
}