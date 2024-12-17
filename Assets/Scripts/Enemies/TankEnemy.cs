using UnityEngine;
using UnityEngine.AI;

public class TankEnemy : Enemy
{
    public TankEnemy()
    {
        health = 200;
        speed = 3;
        acceleration = 4;
        armor = 50f;
    }
    public override void Move()
    {
        agent.SetDestination(target.position);
    }
}