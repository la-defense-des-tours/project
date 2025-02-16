using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class FlyingEnemy : Enemy
    {
        public override float health { get; set; }  = 100;
        public override float speed { get; set; } = 4;
        public override float acceleration { get; set; } = 8;

        public override void SetupSpeed()
        {
            agent.speed = speed;
            agent.acceleration = acceleration;
        }
    }
}