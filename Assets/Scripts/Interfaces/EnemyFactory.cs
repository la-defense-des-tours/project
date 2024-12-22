using UnityEngine;

public interface EnemyFactory
{
    public abstract Enemy CreateEnemy();
    public abstract void Notify();
}
