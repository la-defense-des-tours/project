using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Enemies;

namespace Assets.Scripts.LaDefenseDesTours.Waves
{
    public class Wave_2 : BaseWave
    {
        private readonly EnemyFactory walkingEnemyFactory;
        private readonly EnemyFactory flyingEnemyFactory;
        private readonly EnemyFactory tankEnemyFactory;
        public Wave_2(EnemyFactory walkingEnemyFactory, EnemyFactory flyingEnemyFactory, EnemyFactory tankEnemyFactory)
        {
            difficulty = 2;
            this.walkingEnemyFactory = walkingEnemyFactory;
            this.flyingEnemyFactory = flyingEnemyFactory;
            this.tankEnemyFactory = tankEnemyFactory;
        }

        public override void SpawnEnemies(Vector3 targetPosition)
        {
            int walkingEnemyCount = 3 * difficulty;
            int tankEnemyCount = 3 * difficulty;
            int flyingEnemyCount = 3 * difficulty;

            for (int i = 0; i < walkingEnemyCount; i++)
            {
                var enemy = this.walkingEnemyFactory.CreateEnemy();
                enemy.Move(targetPosition);
                Debug.Log($"Wave 2: Spawned WalkingEnemy {i + 1}/{walkingEnemyCount}.");
            }

            for (int i = 0; i < tankEnemyCount; i++)
            {
                var enemy = this.tankEnemyFactory.CreateEnemy();
                enemy.Move(targetPosition);
                Debug.Log($"Wave 2: Spawned TankEnemy {i + 1}/{tankEnemyCount}.");
            }

            for (int i = 0; i < flyingEnemyCount; i++)
            {
                var enemy = this.flyingEnemyFactory.CreateEnemy();
                enemy.Move(targetPosition);
                Debug.Log($"Wave 2: Spawned FlyingEnemy {i + 1}/{flyingEnemyCount}.");
            }
        }
    }
}
