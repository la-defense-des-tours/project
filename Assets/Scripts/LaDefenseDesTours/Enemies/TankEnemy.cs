using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class TankEnemy : Enemy
    {
        TankEnemy()
        {
            InitializeStats(650f, 1.25f, 2.5f, 0.2f, 5f, 0.3f,
                LevelManager.instance != null ? LevelManager.instance.GetLevel() : 1);
        }
    }
}