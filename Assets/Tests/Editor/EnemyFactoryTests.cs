using Assets.Scripts.LaDefenseDesTours.Enemies;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using NUnit.Framework;
using UnityEngine;

public class EnemyFactoryTests
{
    readonly EnemyFactory flyingFactory = new FlyingEnemyFactory();
    readonly EnemyFactory tankFactory = new TankEnemyFactory();
    readonly EnemyFactory walkingFactory = new WalkingEnemyFactory();

    public Enemy Creator(EnemyFactory enemyFactory)
    {
        return enemyFactory.CreateEnemy();
    }

    [Test]
    public void FlyingFactory_CreatesFlyingEnemy()
    {
        Enemy enemy = Creator(flyingFactory);
        Assert.IsInstanceOf<FlyingEnemy>(enemy);
    }

    [Test]
    public void TankFactory_CreatesTankEnemy()
    {
        Enemy enemy = Creator(tankFactory);
        Assert.IsInstanceOf<TankEnemy>(enemy);
    }

    [Test]
    public void WalkingFactory_CreatesWalkingEnemy()
    {
        Enemy enemy = Creator(walkingFactory);
        Assert.IsInstanceOf<WalkingEnemy>(enemy);
    }
}