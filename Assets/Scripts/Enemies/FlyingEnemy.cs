using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy : Enemy
{
    public FlyingEnemy()
    {
        health = 50;
        speed = 7;
        acceleration = 14;
        armor = 0;
    }
    public override void Move()
    {
        agent.SetDestination(target.position);
    }
}