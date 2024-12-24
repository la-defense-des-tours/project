using UnityEngine;
using UnityEngine.AI;

public class WalkingEnemy : MonoBehaviour, Enemy
{
    private NavMeshAgent agent;
    private float health = 200f;
    private float speed = 3f;
    private float acceleration = 6f;

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
    public Enemy Clone() // Voir au niveau FPS, ou rajouter un check pour ne cloner (ATTENTION: chaque clone)
    {
        Enemy clone = Instantiate(this, Vector3.zero, Quaternion.identity); // A voir ici, par defaut il spawn a la position par defaut du prefab (tester)
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