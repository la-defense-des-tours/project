using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyFactory walkingEnemyFactory;
    [SerializeField] private EnemyFactory flyingEnemyFactory;
    [SerializeField] private EnemyFactory tankEnemyFactory;

    public void Start()
    {
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        Enemy walkingEnemy = walkingEnemyFactory.CreateEnemy();
        Enemy flyingEnemy = flyingEnemyFactory.CreateEnemy();
        Enemy tankEnemy = tankEnemyFactory.CreateEnemy();
    }
}