using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Waves;

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
            Wave wave1 = new Wave_1(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory, this);
            Wave wave2 = new Wave_2(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory, this);
            Wave wave3 = new Wave_3(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory, this);
            Wave wave4 = new Wave_4(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory, bossEnemyFactory, this);

            wave1.SetNext(wave2);
            wave2.SetNext(wave3);
            wave3.SetNext(wave4);
            wave4.SetNext(wave1);

            waveKeyBindings = new Dictionary<KeyCode, Wave>
            {
                { KeyCode.Alpha1, wave1 },
                { KeyCode.Alpha2, wave2 },
                { KeyCode.Alpha3, wave3 },
                { KeyCode.Alpha4, wave4 }
            };
        }

        private void Update()
        {
            foreach (var binding in waveKeyBindings)
            {
                if (Input.GetKeyDown(binding.Key))
                {
                    binding.Value.GenerateWave(target.position);
                    Debug.Log($"Starting wave {binding.Key}");
                }
            }
        }
    }
}