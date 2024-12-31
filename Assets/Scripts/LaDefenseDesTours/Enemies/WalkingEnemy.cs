using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class WalkingEnemy : MonoBehaviour, Enemy
    {
        private State currentState;
        private NavMeshAgent agent;
        private float health = 200f;
        private float speed = 3f;
        private float acceleration = 6f;

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
        public Enemy Clone() // Voir au niveau FPS, ou rajouter un check pour ne cloner (ATTENTION: chaque clone)
        {
            Enemy clone = Instantiate(this, Vector3.zero, Quaternion.identity); // A voir ici, par defaut il spawn a la position par defaut du prefab (tester)
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