using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.LaDefenseDesTours
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
