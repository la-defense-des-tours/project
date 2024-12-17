using UnityEngine;

public class TankEnemyFactory : EnemyFactory
{
    public Enemy CreateEnemy()
    {
        Notify();
        return new TankEnemy();
    }
    public void Notify()
    {
        Debug.Log("Flying enemy created!");
    }
}