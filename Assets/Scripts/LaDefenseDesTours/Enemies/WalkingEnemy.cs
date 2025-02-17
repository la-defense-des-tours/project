using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class WalkingEnemy : Enemy
    {
        WalkingEnemy()
        {
            maxHealth = 200f;
            speed = 2.5f;
            acceleration = 5f;
        }
        public override void SetupSpeed()
        {
            agent.speed = speed;
            agent.acceleration = acceleration;
            animator.speed = speed / 2;
        }
 
    }
}