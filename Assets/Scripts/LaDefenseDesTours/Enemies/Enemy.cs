using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class Enemy : MonoBehaviour, Health
    {
        protected State currentState;
        protected NavMeshAgent agent;
        protected Animator animator;
        public virtual float health { get; set; }
        public virtual float speed { get; set; }
        public virtual float acceleration { get; set; }

        public int experiencePoints = 1000;
        public virtual float maxHealth { get; set; } = 100;
        public event Action OnHealthChanged;

        private ParticleSystem fireEffect;
        private ParticleSystem iceEffect;
        private ParticleSystem lightningEffect;

        public void Awake()
        {
            animator = GetComponent<Animator>();
            SetupNavMeshAgent();

            fireEffect = transform.Find("FireEffect")?.GetComponent<ParticleSystem>();
            iceEffect = transform.Find("IceEffect")?.GetComponent<ParticleSystem>();
            lightningEffect = transform.Find("LightningEffect")?.GetComponent<ParticleSystem>();
        }

        private void Start()
        {
            HealthBar healthBar = FindFirstObjectByType<HealthBar>();
            if (healthBar != null)
            {
                healthBar.SetTarget(this);
            }
            health = maxHealth;
        }

        void Update()
        {
            UpdateState();
            int key = GetNumericKeyPressed();
            if (key != -1)
            {
                HandleEffect(key);
            }

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
                    Debug.Log("Slowed effect applied");
                    break;
                case 2:
                    TransitionTo(new Paralyzed());
                    Debug.Log("Paralyzed effect applied");
                    break;
                case 3:
                    TransitionTo(new Dead());
                    Debug.Log("Dead effect applied");
                    break;
                case 4:
                    TransitionTo(new Burned());
                    Debug.Log("Burned effect applied");
                    break;
                default:
                    Debug.Log("Invalid key pressed. Use 1, 2, 3, or 4 to apply effects.");
                    break;
            }
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
            Debug.Log($"Player took {_damage} damage. Player health: {Player.GetInstance().health}");
        }

        public virtual void Die()
        {
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

            PlayStateEffect(state);
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

        private void PlayStateEffect(State state)
        {
            if (fireEffect != null) fireEffect.Stop();
            if (iceEffect != null) iceEffect.Stop();
            if (lightningEffect != null) lightningEffect.Stop();

            if (state is Burned && fireEffect != null)
            {
                fireEffect.Play();
            }
            else if (state is Paralyzed && iceEffect != null)
            {
                iceEffect.Play();
            }
            else if (state is Slowed && lightningEffect != null)
            {
                lightningEffect.Play();
            }
        }
    }
}