using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Waves;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemyFactory walkingEnemyFactory;
    [SerializeField] private EnemyFactory flyingEnemyFactory;
    [SerializeField] private EnemyFactory tankEnemyFactory;
    [SerializeField] private EnemyFactory bossEnemyFactory;
    [SerializeField] private Transform target;
    private Wave wave1, wave2, wave3, wave4;

    public void Start()
    {
        SetupWaves();
    }

    [ContextMenu("Start Wave")] // Pour mes tests sur scène Prototype
    public void StartWave()
    {
        Generate(wave1);
    }

    private void SetupWaves()
    {
        wave1 = new Wave_1(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory, this);
        wave2 = new Wave_2(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory, this);
        wave3 = new Wave_3(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory, this);
        wave4 = new Wave_4(walkingEnemyFactory, flyingEnemyFactory, tankEnemyFactory, bossEnemyFactory, this);

        // Chain of responsibility pattern : wave1 -> wave2 -> wave3 -> wave4 -> wave1
        wave1.SetNext(wave2);
        wave2.SetNext(wave3);
        wave3.SetNext(wave4);
        wave4.SetNext(wave1);
    }

    public void Generate(Wave wave)
    {
        wave.GenerateWave(target.position);
    }
}
