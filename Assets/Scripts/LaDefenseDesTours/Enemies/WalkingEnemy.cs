using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class WalkingEnemy : MonoBehaviour, Enemy
    {
        private State currentState;
        private NavMeshAgent agent;
        private Animator animator;
        private float health = 200f;
        private float speed = 2.5f;
        private float acceleration = 5;

        void Awake()
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
                animator.speed = speed / 2;
                agent.SetDestination(destination);
            }
        }
        public Enemy Clone(Transform spawnPoint) // Voir au niveau FPS, ou rajouter un check pour ne cloner (ATTENTION: chaque clone)
        {
            Enemy clone = Instantiate(this, spawnPoint.position, Quaternion.identity); // A voir ici, par defaut il spawn a la position par defaut du prefab (tester)
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
    }
}