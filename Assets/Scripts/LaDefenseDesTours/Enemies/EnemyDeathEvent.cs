using System;

namespace LaDefenseDesTours.Enemies
{
    public static class EnemyDeathEvent
    {
        public static event Action<int> OnEnemyDeath;

        public static void EnemyDied(int rewardAmount)
        {
            OnEnemyDeath?.Invoke(rewardAmount);
        }
    }
}
