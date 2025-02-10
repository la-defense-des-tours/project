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
        private readonly float speed = 2.5f;
        private readonly float acceleration = 5;

        void Awake()
        {
            animator = GetComponent<Animator>();
            SetupNavMeshAgent();
        }
        void Update() // Tests pour les effets
        {
            UpdateState();
            switch (Input.inputString)
            {
                case "s":
                    TransitionTo(new Slowed());
                    break;
                case "p":
                    TransitionTo(new Paralyzed());
                    break;
                case "d":
                    TransitionTo(new Dead());
                    break;
                case "b":
                    TransitionTo(new Burned());
                    break;
            }
        }
        public void SetupNavMeshAgent()
        {
            if (gameObject.GetComponent<NavMeshAgent>() == null)
                agent = gameObject.AddComponent<NavMeshAgent>();
            else
                agent = gameObject.GetComponent<NavMeshAgent>();
            SetupSpeed();
        }
        public void Move(Vector3 destination)
        {
            if (currentState is not Paralyzed)
                agent.SetDestination(destination);
        }
        public Enemy Clone(Transform spawnPoint)
        {
            Enemy clone = Instantiate(this, spawnPoint.position, Quaternion.identity);
            clone.SetupNavMeshAgent();
            return clone;
        }
        public void SetupSpeed()
        {
            agent.speed = speed;
            agent.acceleration = acceleration;
            animator.speed = speed / 2;
        }
        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
                TransitionTo(new Dead());
        }
        public void DealDamage(double damage)
        {
            Player.GetInstance().TakeDamage(damage); 
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
            return agent.speed;
        }
        public void SetSpeed(float _speed)
        {
            agent.speed = _speed;
            animator.speed = _speed / 2;
        }
    }
}