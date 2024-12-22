using UnityEngine;

public abstract class EnemyFactory: MonoBehaviour
{
    public abstract Enemy CreateEnemy();
    public abstract void Notify();
}
