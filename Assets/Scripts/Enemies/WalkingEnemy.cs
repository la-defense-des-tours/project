using UnityEngine;
using UnityEngine.AI;

public class WalkingEnemy : Enemy
{
    public WalkingEnemy()
    {
        health = 100;
        speed = 4;
        acceleration = 8;
        armor = 10;
    }
    public override void Move()
    {
        agent.SetDestination(target.position);
    }
}