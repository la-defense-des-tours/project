using UnityEngine;
using UnityEngine.AI;

public class WalkingEnemy : Enemy
{
    public WalkingEnemy()
    {
        health = 100;
        speed = 5;
        acceleration = 8;
    }
    public override void Move()
    {
        agent.SetDestination(target.position);
    }
}