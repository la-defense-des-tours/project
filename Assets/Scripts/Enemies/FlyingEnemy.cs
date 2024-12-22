using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy : MonoBehaviour, Enemy
{
    private NavMeshAgent agent;

    public void Awake()
    {
        if (gameObject.GetComponent<NavMeshAgent>() == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }
        else
        {
            agent = gameObject.GetComponent<NavMeshAgent>();
        }
        agent.speed = 6;
        agent.acceleration = 12;
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
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}