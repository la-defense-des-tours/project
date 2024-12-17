using UnityEngine;

public class FlyingEnemyFactory : EnemyFactory
{
    public override Enemy CreateEnemy()
    {
        return new FlyingEnemy();
    }
    public override void Notify()
    {
        Debug.Log("Flying enemy created!");
    }
}