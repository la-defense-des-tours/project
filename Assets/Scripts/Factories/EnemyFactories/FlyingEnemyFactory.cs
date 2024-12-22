using UnityEngine;

public class FlyingEnemyFactory : EnemyFactory
{
    [SerializeField] private FlyingEnemy flyingEnemy;
    public override Enemy CreateEnemy()
    {
        Notify();
        GameObject instance = Instantiate(flyingEnemy.gameObject);
        return instance.GetComponent<FlyingEnemy>();
    }
    public override void Notify()
    {
        Debug.Log("Flying enemy created!");
    }
}