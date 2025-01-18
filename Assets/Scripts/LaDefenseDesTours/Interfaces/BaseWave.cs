using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class BaseWave : Wave
    {
        protected MonoBehaviour coroutineRunner;
        protected Wave nextWave;
        protected int difficulty;
        protected bool isSpawning = false;
        protected int totalEnemies;
        protected float timeBetweenSpawns = 1.5f;
        protected float timeBetweenWave = 5f;
        protected Vector3 targetPosition;
        public List<Enemy> spawnedEnemies = new List<Enemy>();

        public Wave SetNext(Wave nextWave)
        {
            this.nextWave = nextWave;
            return nextWave;
        }

        public virtual void GenerateWave(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
            Debug.Log($"Wave {GetType().Name} started with difficulty {difficulty}.");
            SpawnEnemies(targetPosition);
        }

        public abstract void SpawnEnemies(Vector3 targetPosition);

        protected virtual void OnWaveCompleted()
        {
            Debug.Log($"Wave {GetType().Name} completed!");
            if (nextWave != null)
            {
                coroutineRunner.StartCoroutine(WaitForNextWave());
            }
        }

        private IEnumerator WaitForNextWave()
        {
            Debug.Log($"Waiting {timeBetweenWave} seconds for next wave...");
            yield return new WaitForSeconds(timeBetweenWave);
            nextWave.GenerateWave(targetPosition);
        }
    }
}