using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

        [SerializeField] private Slider healthBar;

        public int experiencePoints = 1000;

        public virtual float maxHealth { get; set; } = 100;

        public void Awake()
        {
            animator = GetComponent<Animator>();
            SetupNavMeshAgent();
        }

        private void Start()
        {
            health = maxHealth;
            if (healthBar != null)
            {
                healthBar.maxValue = maxHealth;
                healthBar.value = health;
            }
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

            if (animator != null)
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
            if (currentState is Dead)
                return;

            health -= damage;
            if (healthBar != null)
            {
                healthBar.value = health; 
            }
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
            EnemyDeathEvent.EnemyDied(experiencePoints);
            Destroy(gameObject);
        }
        public void TransitionTo(State state)
        {
            if (currentState is Dead)
                return;

            currentState?.OnStateExit();
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