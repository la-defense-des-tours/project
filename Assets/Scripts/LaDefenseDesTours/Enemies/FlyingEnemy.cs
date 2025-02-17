using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class FlyingEnemy : Enemy
    {
        public FlyingEnemy()
        {
            health = 100;
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