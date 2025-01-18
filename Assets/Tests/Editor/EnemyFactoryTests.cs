using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Factories;
using Assets.Scripts.LaDefenseDesTours.Enemies;

public class EnemyFactoryTests
{
    private WalkingEnemyFactory walkingEnemyFactory;
    private FlyingEnemyFactory flyingEnemyFactory;
    private TankEnemyFactory tankEnemyFactory;
    private BossEnemyFactory bossEnemyFactory;
    private GameObject enemyPrefab;
    private WalkingEnemy walkingEnemyComponent;
    private FlyingEnemy flyingEnemyComponent;
    private TankEnemy tankEnemyComponent;
    private BossEnemy bossEnemyComponent;

    [SetUp]
    public void Setup()
    {
        walkingEnemyFactory = new GameObject().AddComponent<WalkingEnemyFactory>();
        enemyPrefab = new GameObject("WalkingEnemyPrefab");
        walkingEnemyComponent = enemyPrefab.AddComponent<WalkingEnemy>();
        var field = typeof(WalkingEnemyFactory).GetField("walkingEnemy", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(walkingEnemyFactory, walkingEnemyComponent);

        flyingEnemyFactory = new GameObject().AddComponent<FlyingEnemyFactory>();
        enemyPrefab = new GameObject("FlyingEnemyPrefab");
        flyingEnemyComponent = enemyPrefab.AddComponent<FlyingEnemy>();
        field = typeof(FlyingEnemyFactory).GetField("flyingEnemy", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(flyingEnemyFactory, flyingEnemyComponent);

        tankEnemyFactory = new GameObject().AddComponent<TankEnemyFactory>();
        enemyPrefab = new GameObject("TankEnemyPrefab");
        tankEnemyComponent = enemyPrefab.AddComponent<TankEnemy>();
        field = typeof(TankEnemyFactory).GetField("tankEnemy", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(tankEnemyFactory, tankEnemyComponent);

        bossEnemyFactory = new GameObject().AddComponent<BossEnemyFactory>();
        enemyPrefab = new GameObject("BossEnemyPrefab");
        bossEnemyComponent = enemyPrefab.AddComponent<BossEnemy>();
        field = typeof(BossEnemyFactory).GetField("bossEnemy", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        field.SetValue(bossEnemyFactory, bossEnemyComponent);
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(walkingEnemyFactory.gameObject);
        Object.DestroyImmediate(enemyPrefab);
    }

    [Test]
    public void WalkingFactory_ReturnsWalkingEnemy()
    {
        Enemy enemy = walkingEnemyFactory.CreateEnemy();

        Assert.IsNotNull(enemy);
        Assert.IsTrue(enemy is WalkingEnemy);
    }

    [Test]
    public void Notify_WalkingEnemy()
    {
        string expectedMessage = "Walking enemy created!";

        LogAssert.Expect(LogType.Log, expectedMessage);
        walkingEnemyFactory.Notify();
    }

    [Test]
    public void FlyingFactory_ReturnsFlyingEnemy()
    {
        Enemy enemy = flyingEnemyFactory.CreateEnemy();

        Assert.IsNotNull(enemy);
        Assert.IsTrue(enemy is FlyingEnemy);
    }

    [Test]
    public void Notify_FlyingEnemy()
    {
        string expectedMessage = "Flying enemy created!";

        LogAssert.Expect(LogType.Log, expectedMessage);
        flyingEnemyFactory.Notify();
    }

    [Test]
    public void TankFactory_ReturnsTankEnemy()
    {
        Enemy enemy = tankEnemyFactory.CreateEnemy();

        Assert.IsNotNull(enemy);
        Assert.IsTrue(enemy is TankEnemy);
    }

    [Test]
    public void Notify_TankEnemy()
    {
        string expectedMessage = "Tank enemy created!";

        LogAssert.Expect(LogType.Log, expectedMessage);
        tankEnemyFactory.Notify();
    }

    [Test]
    public void BossFactory_ReturnsBossEnemy()
    {
        Enemy enemy = bossEnemyFactory.CreateEnemy();

        Assert.IsNotNull(enemy);
        Assert.IsTrue(enemy is BossEnemy);
    }

    [Test]
    public void Notify_BossEnemy()
    {
        string expectedMessage = "Boss enemy created!";

        LogAssert.Expect(LogType.Log, expectedMessage);
        bossEnemyFactory.Notify();
    }

}