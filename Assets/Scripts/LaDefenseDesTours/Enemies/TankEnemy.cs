using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class TankEnemy : Enemy
    {
        TankEnemy()
        {
            InitializeStats(350f, 1.25f, 2f, 0.1f, 2.5f, 0.3f,
                LevelManager.instance != null ? LevelManager.instance.GetLevel() : 1);
        }
    }
}