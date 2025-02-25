using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class WalkingEnemy : Enemy
    {
        WalkingEnemy()
        {
            maxHealth = 2000000000000000000f;
            speed = 4.5f;
            acceleration = 5f;
        }
        public override void SetupSpeed()
        {
            agent.speed = speed;
            agent.acceleration = acceleration;
            animator.speed = speed / 2;
        }

        public override void SetSpeed(float _speed)
        {
            agent.speed = _speed;
            animator.speed = _speed / 2;
        }
    }
}