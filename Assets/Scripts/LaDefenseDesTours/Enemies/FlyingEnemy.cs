using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class FlyingEnemy : Enemy
    {
        FlyingEnemy()
        {
            InitializeStats(100f, 1.1f, 6f, 0.3f, 8f, 0.9f);
        }
        public override void SetupSpeed()
        {
            agent.speed = speed;
            agent.acceleration = acceleration;
        }
    }
}