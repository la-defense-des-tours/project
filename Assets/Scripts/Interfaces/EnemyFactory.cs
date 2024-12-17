using UnityEngine;

public interface EnemyFactory
{
    public Enemy CreateEnemy();
    public void Notify();
}
