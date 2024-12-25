using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class WalkingEnemy : MonoBehaviour, Enemy
{
    private NavMeshAgent agent;
    private float health = 200f;
    public void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = 4;
        agent.acceleration = 8;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Clone();
        }
    }
    public void Move(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    public Enemy Clone() // Voir au niveau FPS, ou rajouter un check pour ne cloner (ATTENTION: chaque clone)
    {
        Enemy enemyClone = Instantiate(this);
        return enemyClone;
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
}