using UnityEngine;

public class FlyingEnemyFactory : EnemyFactory
{
    public Enemy CreateEnemy()
    {
        Notify();
        return new FlyingEnemy();
    }
    public void Notify()
    {
        Debug.Log("Flying enemy created!");
    }
}