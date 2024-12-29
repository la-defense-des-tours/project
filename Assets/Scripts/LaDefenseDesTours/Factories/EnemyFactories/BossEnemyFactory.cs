using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;


public class BossEnemyFactory : EnemyFactory
{
    [SerializeField] private BossEnemy bossEnemy;
    public override Enemy CreateEnemy()
    {
        Notify();
        GameObject instance = Instantiate(bossEnemy.gameObject);
        return instance.GetComponent<BossEnemy>();
    }
    public override void Notify()
    {
        Debug.Log("Boss enemy created!");
    }
}
