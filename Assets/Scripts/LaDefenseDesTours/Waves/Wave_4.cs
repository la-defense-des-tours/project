using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Enemies;

namespace Assets.Scripts.LaDefenseDesTours.Waves
{
public class Wave_4 : BaseWave
    {
        private readonly EnemyFactory walkingEnemyFactory;
        private readonly EnemyFactory flyingEnemyFactory;
        private readonly EnemyFactory tankEnemyFactory;
        private readonly EnemyFactory bossEnemyFactory;

        public Wave_4(EnemyFactory walkingEnemyFactory, EnemyFactory flyingEnemyFactory, EnemyFactory tankEnemyFactory, EnemyFactory bossEnemyFactory)
        {
            difficulty = 4;
            this.walkingEnemyFactory = walkingEnemyFactory;
            this.flyingEnemyFactory = flyingEnemyFactory;
            this.tankEnemyFactory = tankEnemyFactory;
            this.bossEnemyFactory = bossEnemyFactory;
        }

        public override void SpawnEnemies(Vector3 targetPosition)
        {
            int walkingEnemyCount = 8 * difficulty;
            int tankEnemyCount = 8 * difficulty;
            int flyingEnemyCount = 8 * difficulty;

            for (int i = 0; i < walkingEnemyCount; i++)
            {
                var enemy = this.walkingEnemyFactory.CreateEnemy();
                enemy.Move(targetPosition);
                Debug.Log($"Wave 4: Spawned WalkingEnemy {i + 1}/{walkingEnemyCount}.");
            }

            for (int i = 0; i < tankEnemyCount; i++)
            {
                var enemy = this.tankEnemyFactory.CreateEnemy();
                enemy.Move(targetPosition);
                Debug.Log($"Wave 4: Spawned TankEnemy {i + 1}/{tankEnemyCount}.");
            }

            for (int i = 0; i < flyingEnemyCount; i++)
            {
                var enemy = this.flyingEnemyFactory.CreateEnemy();
                enemy.Move(targetPosition);
                Debug.Log($"Wave 4: Spawned FlyingEnemy {i + 1}/{flyingEnemyCount}.");
            }

            // Spawn le boss
            var boss = this.bossEnemyFactory.CreateEnemy();
            boss.Move(targetPosition);
            Debug.Log($"Wave 4: Spawned Boss.");
        }
    }
}
