using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class FlyingEnemy : Enemy
    {
        FlyingEnemy()
        {
            InitializeStats(200f, 1.1f, 8f, 0.3f, 8f, 0.9f, 
                LevelManager.instance != null ? LevelManager.instance.GetLevel() : 1);
        }
        public override void SetupSpeed()
        {
            agent.speed = speed;
            agent.acceleration = acceleration;
        }
    }
}