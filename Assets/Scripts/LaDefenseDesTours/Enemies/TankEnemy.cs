using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class TankEnemy : Enemy
    {
        public TankEnemy()
        {
            health = 350;
            speed = 1.5f;
            acceleration = 2.5f;
        }
    }
}