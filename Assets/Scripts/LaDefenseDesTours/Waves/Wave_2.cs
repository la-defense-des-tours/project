using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using System.Collections;

namespace Assets.Scripts.LaDefenseDesTours.Waves
{
    public class Wave_2 : BaseWave
    {
        private readonly EnemyFactory walkingEnemyFactory;
        private readonly EnemyFactory flyingEnemyFactory;
        private readonly EnemyFactory tankEnemyFactory;

        public Wave_2(EnemyFactory walkingEnemyFactory, EnemyFactory flyingEnemyFactory, EnemyFactory tankEnemyFactory, MonoBehaviour coroutineRunner)
        {
            difficulty = 2;
            this.walkingEnemyFactory = walkingEnemyFactory;
            this.flyingEnemyFactory = flyingEnemyFactory;
            this.tankEnemyFactory = tankEnemyFactory;
            this.coroutineRunner = coroutineRunner;
        }

        public override void SpawnEnemies(Vector3 targetPosition)
        {
            if (!isSpawning)
            {
                isSpawning = true;
                coroutineRunner.StartCoroutine(SpawnWaveRoutine(targetPosition));
            }
        }

        private IEnumerator SpawnWaveRoutine(Vector3 targetPosition)
        {
            totalEnemies = 6 * difficulty;
            int enemiesSpawned = 0;

            while (enemiesSpawned < totalEnemies)
            {
                float random = Random.Range(0f, 1f);
                Enemy enemy;

                if (random < 0.5f)
                    enemy = walkingEnemyFactory.CreateEnemy();
                else if (random < 0.85f)
                    enemy = flyingEnemyFactory.CreateEnemy();
                else
                    enemy = tankEnemyFactory.CreateEnemy();

                enemy.Move(targetPosition);
                spawnedEnemies.Add(enemy);
                enemiesSpawned++;

                Debug.Log($"Wave 2: Spawned enemy {enemiesSpawned}/{totalEnemies}");
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            isSpawning = false;
            OnWaveCompleted();
        }
    }
}
