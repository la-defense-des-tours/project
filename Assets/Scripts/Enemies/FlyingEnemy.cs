using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy : Enemy
{
    public FlyingEnemy()
    {
        health = 50;
        speed = 10;
        acceleration = 16;
        armor = 0f;
    }
    public override void Move()
    {
        agent.SetDestination(target.position);
    }
}