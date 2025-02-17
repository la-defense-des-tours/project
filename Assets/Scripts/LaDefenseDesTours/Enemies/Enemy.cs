using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class Enemy : MonoBehaviour
    {
        protected State currentState;
        protected NavMeshAgent agent;
        protected Animator animator;
        public virtual float health { get; set; }
        public virtual float speed { get; set; }
        public virtual float acceleration { get; set; }

        public void Awake()
        {
            animator = GetComponent<Animator>();
            SetupNavMeshAgent();
        }

        void Update()
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

            if (currentState is not Dead)
                CheckArrival();
        }
        public virtual void SetupNavMeshAgent()
        {
            if (gameObject.GetComponent<NavMeshAgent>() == null)
                agent = gameObject.AddComponent<NavMeshAgent>();
            else
                agent = gameObject.GetComponent<NavMeshAgent>();

            SetupSpeed();
        }

        public virtual void SetupSpeed()
        {
            agent.speed = speed;
            agent.acceleration = acceleration;
            animator.speed = speed;
        }
        public virtual void Move(Vector3 destination)
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

        public virtual void TakeDamage(float damage)
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
        public virtual void Die()
        {
            Destroy(gameObject);
        }
        public void TransitionTo(State state)
        {
            currentState = state;
            currentState.SetContext(this);
            currentState.OnStateEnter();
        }
        public void UpdateState()
        {
            currentState?.ApplyEffect();
        }
        public virtual float GetSpeed()
        {
            return agent.speed;
        }
        public virtual void SetSpeed(float _speed)
        {
            agent.speed = _speed;

            if (animator != null)
                animator.speed = _speed;
        }
        public virtual void CheckArrival()
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    DealDamage(health);
                    TransitionTo(new Dead());
                }
            }
        }

    }
}