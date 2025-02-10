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
        private readonly float speed = 4;
        private readonly float acceleration = 8;

        public void Awake()
        {
            SetupNavMeshAgent();
        }
        void Update() // Tests pour les effets
        {
            UpdateState();
            CheckArrival();

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
        }
        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
                TransitionTo(new Dead());
        }
        public void DealDamage(float damage)
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
        }
        public void CheckArrival()
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    DealDamage(health);
                    Die();
                }
            }
        }
    }
}