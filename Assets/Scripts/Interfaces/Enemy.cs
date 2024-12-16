using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected float health;
    protected float speed;
    protected float acceleration;
    public Transform target;
    protected NavMeshAgent agent;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = this.speed;
        agent.acceleration = this.acceleration;
    }
    protected virtual void Update()
    {
        Move();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public abstract void Move();

    public abstract Enemy Clone();
}