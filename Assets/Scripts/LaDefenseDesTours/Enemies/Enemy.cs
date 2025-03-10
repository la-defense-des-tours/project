using System;
using System.Collections;
using Assets.Scripts.LaDefenseDesTours;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace LaDefenseDesTours.Enemies
{
    public abstract class Enemy : MonoBehaviour, Health
    {
        private State currentState;
        protected NavMeshAgent agent;
        protected Animator animator;
        private AudioSource audioSource;

        public virtual float health { get; set; }
        protected float speed { get; set; }
        protected float acceleration { get; set; }
        private int experiencePoints { get; set; }
        public virtual float maxHealth { get; set; }
        public event Action OnHealthChanged;
        private Vector3 attackPosition;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            attackPosition = GameObject.Find("AttackFalcon!").transform.position;
            SetupNavMeshAgent();
        }

        private void Start()
        {
            HealthBar healthBar = FindFirstObjectByType<HealthBar>();
            if (healthBar != null)
                healthBar.SetTarget(this);

            health = maxHealth;
            TransitionTo(new Normal());
        }

        protected void InitializeStats(float baseHealth, float healthFactor, float baseSpeed, float speedFactor, float baseAcceleration, float accelerationFactor, int currentLevel)
        {
            float maxSpeed = baseSpeed * 2;
            float maxAcceleration = baseAcceleration * 2;
            maxHealth = Mathf.RoundToInt(baseHealth * Mathf.Pow(healthFactor, currentLevel - 1));
            speed = Mathf.Min(baseSpeed + (speedFactor * (currentLevel - 1)), maxSpeed);
            acceleration = Mathf.Min(baseAcceleration + (accelerationFactor * (currentLevel - 1)), maxAcceleration);
            experiencePoints = (int)maxHealth / 10;
        }

        void Update()
        {
            UpdateState();

            int key = GetNumericKeyPressed();
            if (key != -1)
                HandleEffect(key);

            if (currentState is not Dead)
                CheckArrival();
        }

        private int GetNumericKeyPressed()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) return 1;
            if (Input.GetKeyDown(KeyCode.Alpha2)) return 2;
            if (Input.GetKeyDown(KeyCode.Alpha3)) return 3;
            if (Input.GetKeyDown(KeyCode.Alpha4)) return 4;
            return -1;
        }

        private void HandleEffect(int key)
        {
            switch (key)
            {
                case 1:
                    TransitionTo(new Slowed());
                    break;
                case 2:
                    TransitionTo(new Paralyzed());
                    break;
                case 3:
                    TransitionTo(new Burned());
                    break;
                case 4:
                    TransitionTo(new Dead());
                    break;
            }
        }

        protected virtual void SetupNavMeshAgent()
        {
            agent = gameObject.GetComponent<NavMeshAgent>() == null
                ? gameObject.AddComponent<NavMeshAgent>()
                : gameObject.GetComponent<NavMeshAgent>();
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

        public virtual void TakeDamage(float damage)
        {
            if (currentState is Dead)
                return;

            health -= damage;
            OnHealthChanged?.Invoke();

            if (health <= 0)
            {
                EnemyDeathEvent.EnemyDied(experiencePoints);
                TransitionTo(new Dead());
            }
        }

        public void DealDamage(float _damage)
        {
            Player.GetInstance().TakeDamage(_damage);
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }

        public void TransitionTo(State state)
        {
            if (currentState is Dead)
                return;

            if (currentState != null && currentState.GetType() == state.GetType())
                return;

            currentState?.OnStateExit();
            currentState = state;
            currentState.SetContext(this);
            currentState.OnStateEnter();
        }

        public IEnumerator PlayDeathSound()
        {
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
                yield return new WaitForSeconds(audioSource.clip.length);
            }
        }

        private void UpdateState()
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

        protected virtual void CheckArrival()
        {
            if (transform.position.x <= attackPosition.x)
            {
                experiencePoints = 0;
                DealDamage(health);
                TakeDamage(health);
            }
        }
    }
}