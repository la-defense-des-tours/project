using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class BossEnemy : MonoBehaviour, Enemy
    {
        private State currentState;
        private NavMeshAgent agent;
        private Animator animator;
        private float health = 1000;
        private float speed = 1;
        private float acceleration = 3;

        // TODO: implement attack behavior of the boss enemy

        public void Awake()
        {
            animator = GetComponent<Animator>();
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
                animator.speed = speed;
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