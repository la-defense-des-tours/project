using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Enemies;

namespace Assets.Scripts.LaDefenseDesTours.Waves
{
    public class Wave_1 : BaseWave
    {
        private readonly EnemyFactory walkingEnemyFactory;
        private readonly EnemyFactory flyingEnemyFactory;
        private readonly EnemyFactory tankEnemyFactory;

        public Wave_1(EnemyFactory walkingEnemyFactory, EnemyFactory flyingEnemyFactory, EnemyFactory tankEnemyFactory)
        {
            difficulty = 1;
            this.walkingEnemyFactory = walkingEnemyFactory;
            this.flyingEnemyFactory = flyingEnemyFactory;
            this.tankEnemyFactory = tankEnemyFactory;

        }

        public override void SpawnEnemies(Vector3 targetPosition)
        {
            int walkingEnemyCount = 2 * difficulty;
            int tankEnemyCount = 2 * difficulty;
            int flyingEnemyCount = 2 * difficulty;

            for (int i = 0; i < walkingEnemyCount; i++)
            {
                var enemy = this.walkingEnemyFactory.CreateEnemy();
                enemy.Move(targetPosition);
                Debug.Log($"Wave 1: Spawned WalkingEnemy {i + 1}/{walkingEnemyCount}.");
            }

            for (int i = 0; i < tankEnemyCount; i++)
            {
                var enemy = this.tankEnemyFactory.CreateEnemy();
                enemy.Move(targetPosition);
                Debug.Log($"Wave 1: Spawned TankEnemy {i + 1}/{tankEnemyCount}.");
            }

            for (int i = 0; i < flyingEnemyCount; i++)
            {
                var enemy = this.flyingEnemyFactory.CreateEnemy();
                enemy.Move(targetPosition);
                Debug.Log($"Wave 1: Spawned FlyingEnemy {i + 1}/{flyingEnemyCount}.");
            }
        }
    }
}
