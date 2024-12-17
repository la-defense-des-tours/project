using UnityEngine;

public class WalkingEnemyFactory : EnemyFactory
{
    public Enemy CreateEnemy()
    {
        Notify();
        return new WalkingEnemy();
    }
    public void Notify()
    {
        Debug.Log("Walking enemy created!");
    }
}