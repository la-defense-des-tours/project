using System.Collections;
using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Waves
{
    public class Wave_4 : BaseWave
    {
        private readonly EnemyFactory walkingEnemyFactory;
        private readonly EnemyFactory flyingEnemyFactory;
        private readonly EnemyFactory tankEnemyFactory;
        private readonly EnemyFactory bossEnemyFactory;

        public Wave_4(EnemyFactory walkingEnemyFactory, EnemyFactory flyingEnemyFactory,EnemyFactory tankEnemyFactory, EnemyFactory bossEnemyFactory, MonoBehaviour coroutineRunner)
        {
            difficulty = 1;
            this.walkingEnemyFactory = walkingEnemyFactory;
            this.flyingEnemyFactory = flyingEnemyFactory;
            this.tankEnemyFactory = tankEnemyFactory;
            this.bossEnemyFactory = bossEnemyFactory;
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
            totalEnemies = 8 * difficulty;
            int enemiesSpawned = 0;

            while (enemiesSpawned < totalEnemies)
            {
                float random = Random.Range(0f, 1f);
                Enemy enemy;

                // if (random < 0.5f)
                //     enemy = walkingEnemyFactory.CreateEnemy();
                // else if (random < 0.85f)
                //     enemy = flyingEnemyFactory.CreateEnemy();
                // else
                //     enemy = tankEnemyFactory.CreateEnemy();
                enemy = walkingEnemyFactory.CreateEnemy();

                targetPosition.z = Random.Range(-3, 3);
                enemy.Move(targetPosition);
                spawnedEnemies.Add(enemy);
                enemiesSpawned++;

                Debug.Log($"Wave 4: Spawned enemy {enemiesSpawned}/{totalEnemies}");
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            Enemy boss = bossEnemyFactory.CreateEnemy();
            boss.Move(targetPosition);
            spawnedEnemies.Add(boss);
            Debug.Log("Wave 4: Spawned Boss");
            yield return new WaitForSeconds(timeBetweenSpawns);

            isSpawning = false;
            difficulty++;
            OnWaveCompleted();
        }
    }
}