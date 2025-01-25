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

        void Update()
        {            
            switch (Input.inputString)
            {
                case "S":
                    TransitionTo(new Slowed());
                    break;
                case "P":
                    TransitionTo(new Paralyzed());
                    break;
                case "D":
                    TransitionTo(new Dead());
                    break;
                case "B":
                    TransitionTo(new Burned());
                    break;
            }
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
            animator.speed = speed / 2;
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