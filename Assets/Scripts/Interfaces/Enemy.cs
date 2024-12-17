using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected float health;
    protected float speed;
    protected float acceleration;
    protected float armor;
    protected Transform target;
    protected NavMeshAgent agent;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.acceleration = acceleration;
    }
    protected virtual void Update()
    {
        Move();
    }
    public abstract void Move();
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
    public virtual Enemy Clone()
    {
        return (Enemy)MemberwiseClone();
    }

}