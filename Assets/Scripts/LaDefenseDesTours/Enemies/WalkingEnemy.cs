using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class WalkingEnemy : Enemy
    {
        public override float health { get; set; } = 200;
        public override float speed { get; set; } = 2.5f;
        public override float acceleration { get; set; } = 5f;

        public override void SetupSpeed()
        {
            agent.speed = speed;
            agent.acceleration = acceleration;
            animator.speed = speed / 2;
        }

    }
}