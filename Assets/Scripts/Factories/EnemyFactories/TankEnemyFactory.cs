using UnityEngine;

public class TankEnemyFactory : EnemyFactory
{
    [SerializeField] private TankEnemy tankEnemy;
    public override Enemy CreateEnemy()
    {
        Notify();
        GameObject instance = Instantiate(tankEnemy.gameObject);
        return instance.GetComponent<TankEnemy>();
    }
    public override void Notify()
    {
        Debug.Log("Flying enemy created!");
    }
}