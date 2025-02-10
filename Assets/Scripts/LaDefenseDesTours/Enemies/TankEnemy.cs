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
        private readonly float speed = 1.5f;
        private readonly float acceleration = 2.5f;

        public void Awake()
        {
            animator = GetComponent<Animator>();
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
            animator.speed = speed;
        }
        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
                TransitionTo(new Dead());
        }
        public void DealDamage(float _damage)
        {
            Player.GetInstance().TakeDamage(_damage);
            Debug.Log($"Player took {_damage} damage. Player health: {Player.GetInstance().health}");
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