using UnityEngine;

public class WalkingEnemyFactory : EnemyFactory
{
    public Enemy CreateEnemy()
    {
        return new WalkingEnemy();
    }
    public void Notify()
    {
        Debug.Log("Walking enemy created!");
    }
}