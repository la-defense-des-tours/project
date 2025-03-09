using Assets.Scripts.LaDefenseDesTours.Level;

namespace LaDefenseDesTours.Enemies
{
    public class FlyingEnemy : Enemy
    {
        FlyingEnemy()
        {
            InitializeStats(200f, 1.1f, 8f, 0.4f, 10f, 0.9f, 
                LevelManager.instance != null ? LevelManager.instance.GetLevel() : 1);
        }
        public override void SetupSpeed()
        {
            agent.speed = speed;
            agent.acceleration = acceleration;
        }
    }
}