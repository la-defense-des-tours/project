using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using System.Collections;
using Assets.Scripts.LaDefenseDesTours.Level;
using LaDefenseDesTours.Enemies;

namespace Assets.Scripts.LaDefenseDesTours.Waves
{
    public class Wave_2 : Wave
    {
        private readonly EnemyFactory walkingEnemyFactory;
        private readonly EnemyFactory flyingEnemyFactory;
        private readonly EnemyFactory tankEnemyFactory;

        public Wave_2(EnemyFactory walkingEnemyFactory, EnemyFactory flyingEnemyFactory, EnemyFactory tankEnemyFactory, MonoBehaviour coroutineRunner)
        {
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
            totalEnemies = 4 * LevelManager.instance.GetLevel();
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

                targetPosition.z = Random.Range(-3, 3);
                enemy.Move(targetPosition);
                spawnedEnemies.Add(enemy);
                enemiesSpawned++;

                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            isSpawning = false;
            OnWaveCompleted();
        }
    }
}
