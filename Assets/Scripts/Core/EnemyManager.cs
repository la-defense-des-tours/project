using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyFactory walkingEnemyFactory;
    [SerializeField] private EnemyFactory flyingEnemyFactory;
    [SerializeField] private EnemyFactory tankEnemyFactory;
    [SerializeField] private Transform target;

    public void Start()
    {
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        Enemy walkingEnemy = walkingEnemyFactory.CreateEnemy();
        Enemy flyingEnemy = flyingEnemyFactory.CreateEnemy();
        Enemy tankEnemy = tankEnemyFactory.CreateEnemy();

        walkingEnemy.Move(target.position);
        flyingEnemy.Move(target.position);
        tankEnemy.Move(target.position);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        }
    }
}