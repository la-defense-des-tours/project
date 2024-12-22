using UnityEngine;
using UnityEngine.AI;

public class WalkingEnemy : MonoBehaviour, Enemy
{
    private NavMeshAgent agent;
    private float health = 200f;
    public void Awake()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
        agent.speed = 4;
        agent.acceleration = 8;
    }
    public void Move(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    public Enemy Clone()
    {
        return (Enemy)MemberwiseClone();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}