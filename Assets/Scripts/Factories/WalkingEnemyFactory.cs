using UnityEngine;

public class WalkingEnemyFactory : EnemyFactory
{
    public Enemy CreateEnemy()
    {
        return new WalkingEnemy();
    }
}