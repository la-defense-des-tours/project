using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class WalkingEnemy : Enemy
    {
        WalkingEnemy()
        {
            maxHealth = 200f;
            speed = 3f;
            acceleration = 5f;
        }
        public override void SetupSpeed()
        {
            agent.speed = speed;
            agent.acceleration = acceleration;
            animator.speed = speed;
        }
        public override void SetSpeed(float _speed)
        {
            agent.speed = _speed;
            animator.speed = _speed;
        }
    }
}