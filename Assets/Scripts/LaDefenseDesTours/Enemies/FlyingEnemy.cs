using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class FlyingEnemy : Enemy
    {

        FlyingEnemy()
        {
            maxHealth = 100f;
            speed = 4;
            acceleration = 8;
        }
        public override void SetupSpeed()
        {
            agent.speed = speed;
            agent.acceleration = acceleration;
        }
    }
}