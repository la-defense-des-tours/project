using UnityEngine;
using UnityEngine.AI;

public class TankEnemy : MonoBehaviour, Enemy
{
    private NavMeshAgent agent;
    private float health = 350;
    private float speed = 1.5f;
    private float acceleration = 2.5f;

    public void Awake()
    {
        SetupNavMeshAgent();
    }
    public void SetupNavMeshAgent()
    {
        if (gameObject.GetComponent<NavMeshAgent>() == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }
        else
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
        }
        agent.speed = speed;
        agent.acceleration = acceleration;
    }
    public void Move(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    public Enemy Clone()
    {
        Enemy clone = Instantiate(this, Vector3.zero, Quaternion.identity);
        clone.SetupNavMeshAgent();
        return clone;
    }
    public void TakeDamage(float damage)
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