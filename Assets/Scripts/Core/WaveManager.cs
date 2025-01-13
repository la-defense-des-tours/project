using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Waves;
using System.Collections.Generic;
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

        private Dictionary<KeyCode, Wave> waveKeyBindings;

        private void Start()
        {
            SetupWaves();
        }

        private void SetupWaves()
        {
            // Initialization des vagues
            var wave1 = new Wave_1(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory);
            var wave2 = new Wave_2(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory);
            var wave3 = new Wave_3(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory);
            var wave4 = new Wave_4(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory, bossEnemyFactory);

            waveKeyBindings = new Dictionary<KeyCode, Wave>
            {
                { KeyCode.G, wave1 },
                { KeyCode.H, wave2 },
                { KeyCode.J, wave3 },
                { KeyCode.K, wave4 }
            };
        }

        private void Update()
        {
            HandleWaveInput();
        }
        private void HandleWaveInput()
        {
            foreach (var keyWavePair in waveKeyBindings)
            {
                if (Input.GetKeyDown(keyWavePair.Key))
                {
                    Debug.Log($"Starting wave triggered by {keyWavePair.Key}");
                    keyWavePair.Value.GenerateWave(target.position);
                }
            }
        }
    }
}
