using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class BossEnemy : Enemy
    {
        public BossEnemy()
        {
            maxHealth = 1000f;
            speed = 1;
            acceleration = 3;
        }
    }
}