using UnityEngine;
using UnityEngine.AI;

public class TankEnemy : MonoBehaviour, Enemy
{
    private NavMeshAgent agent;

    public void Awake()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
        agent.speed = 2;
        agent.acceleration = 4;
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