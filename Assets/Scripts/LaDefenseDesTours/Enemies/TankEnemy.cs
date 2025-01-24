using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class TankEnemy : MonoBehaviour, Enemy
    {
        private State currentState;
        private NavMeshAgent agent;
        private Animator animator;
        private float health = 350;
        private float speed = 1.5f;
        private float acceleration = 2.5f;

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
        }
        public void UpdateState()
        {
            currentState?.ApplyEffect();
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