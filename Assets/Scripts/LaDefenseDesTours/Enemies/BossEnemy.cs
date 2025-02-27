using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class BossEnemy : Enemy
    {
        public BossEnemy()
        {
            InitializeStats(1000f, 1.5f, 1f, 0.2f, 3f, 0.5f);
        }
    }
}