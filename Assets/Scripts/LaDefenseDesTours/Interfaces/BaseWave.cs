using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class BaseWave : Wave
    {
        private Wave nextWave;
        public static int count = 0; // Static pour que chaque instance de wave ait un id unique
        public int difficulty;

        public List<Enemy> spawnedEnemies = new List<Enemy>();

        public Wave SetNext(Wave nextWave)
        {
            this.nextWave = nextWave;
            return nextWave;
        }

        public virtual void GenerateWave(Vector3 targetPosition)
        {
            Debug.Log($"Wave {GetType().Name} started with difficulty {difficulty}.");
            SpawnEnemies(targetPosition);

            nextWave?.GenerateWave(targetPosition);
        }

        public abstract void SpawnEnemies(Vector3 targetPosition);
    }
}
