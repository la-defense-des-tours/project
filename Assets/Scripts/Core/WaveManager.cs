using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Waves;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private EnemyFactory walkingEnemyFactory;
        [SerializeField] private EnemyFactory flyingEnemyFactory;
        [SerializeField] private EnemyFactory tankEnemyFactory;
        [SerializeField] private EnemyFactory bossEnemyFactory;
        [SerializeField] private Transform target;

        private Wave waveChain;

        private void Start()
        {
            SetupWaves();
            StartWaveSequence();
        }

        private void SetupWaves()
        {
            // Creation du boss
            Enemy bossEnemy = bossEnemyFactory.CreateEnemy();

            // Initialization des vagues
            var wave1 = new Wave_1(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory);
            var wave2 = new Wave_2(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory);
            var wave3 = new Wave_3(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory);
            var wave4 = new Wave_4(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory, bossEnemyFactory);

            // Enchainement des vagues
            waveChain = wave1.SetNext(wave2).SetNext(wave3).SetNext(wave4);
        }

        private void StartWaveSequence()
        {
            Debug.Log("Starting wave sequence...");
            waveChain?.GenerateWave(target.position);
        }
    }
}
