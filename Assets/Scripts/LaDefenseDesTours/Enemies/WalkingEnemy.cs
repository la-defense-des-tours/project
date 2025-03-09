using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class WalkingEnemy : Enemy
    {
        WalkingEnemy()
        {
            InitializeStats(350f, 1.15f, 6f, 0.2f, 6f, 0.5f,
                LevelManager.instance != null ? LevelManager.instance.GetLevel() : 1);
        }
        public override void SetupSpeed()
        {
            agent.speed = speed;
            agent.acceleration = acceleration;
            animator.speed = speed / 3.5f;
        }
        public override void SetSpeed(float _speed)
        {
            agent.speed = _speed;
            animator.speed = _speed / 3.5f;
        }
    }
}