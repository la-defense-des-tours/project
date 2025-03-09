using Assets.Scripts.LaDefenseDesTours.Level;

namespace LaDefenseDesTours.Enemies
{
    public class BossEnemy : Enemy
    {
        BossEnemy()
        {
            InitializeStats(1500f, 1.5f, 2f, 0.2f, 4f, 0.5f,
                LevelManager.instance != null ? LevelManager.instance.GetLevel() : 1);
        }
    }
}