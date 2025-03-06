using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class BossEnemy : Enemy
    {
        BossEnemy()
        {
            InitializeStats(1500f, 1.5f, 1f, 0.2f, 3f, 0.5f,
                LevelManager.instance != null ? LevelManager.instance.GetLevel() : 1);
        }
    }
}