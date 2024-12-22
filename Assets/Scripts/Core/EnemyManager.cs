using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyFactory walkingEnemyFactory;
    [SerializeField] private EnemyFactory flyingEnemyFactory;
    [SerializeField] private EnemyFactory tankEnemyFactory;
    [SerializeField] private Transform target;
    private Enemy walkingEnemy;
    private Enemy flyingEnemy;
    private Enemy tankEnemy;

    public void Start()
    {
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        walkingEnemy = walkingEnemyFactory.CreateEnemy();
        walkingEnemy.Move(target.position);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Enemy walkingClone = walkingEnemy.Clone();
            walkingClone.Move(target.position);
        }
    }
}