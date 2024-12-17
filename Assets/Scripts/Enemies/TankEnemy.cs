using UnityEngine;
using UnityEngine.AI;

public class TankEnemy : Enemy
{
    public TankEnemy()
    {
        health = 300;
        speed = 2;
        acceleration = 4;
        armor = 30;
    }
    public override void Move()
    {
        agent.SetDestination(target.position);
    }
}