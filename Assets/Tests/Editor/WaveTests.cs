using NUnit.Framework;
using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

public class TestWave : Wave
{
    public bool WaveCompletedCalled { get; private set; }
    public TestWave GetNextWave() => (TestWave)nextWave;
    public Vector3 GetTargetPosition() => targetPosition;
    public bool IsSpawning() => isSpawning;
    public int GetDifficulty() => difficulty;

    public override void SpawnEnemies(Vector3 targetPosition)
    {
        isSpawning = true;
        Debug.Log("Spawning enemies...");
    }

    protected override void OnWaveCompleted()
    {
        WaveCompletedCalled = true;
        base.OnWaveCompleted();
    }

    public void SimulateWaveCompletion()
    {
        OnWaveCompleted();
    }
}

public class WaveTests
{
    private TestWave wave;
    private TestWave nextWave;
    private Vector3 testTargetPosition;

    [SetUp]
    public void SetUp()
    {
        wave = new TestWave();
        nextWave = new TestWave();
        testTargetPosition = new Vector3(1, 2, 3);
    }

    [Test]
    public void SpawnedEnemiesList_ShouldBeEmptyInitially()
    {
        Assert.IsEmpty(wave.spawnedEnemies);
    }

    [Test]
    public void SetNext_ShouldSetNextWave()
    {
        Wave result = wave.SetNext(nextWave);

        Assert.AreEqual(nextWave, result);
        Assert.AreEqual(nextWave, wave.GetNextWave());
    }

    [Test]
    public void GenerateWave_ShouldSetTargetPosition()
    {
        wave.GenerateWave(testTargetPosition);
        Assert.AreEqual(testTargetPosition, wave.GetTargetPosition());
    }

    [Test]
    public void GenerateWave_ShouldTriggerSpawnEnemies()
    {
        wave.GenerateWave(testTargetPosition);
        Assert.IsTrue(wave.IsSpawning());
    }

    [Test]
    public void ChainOfResponsibility_ShouldCreateProperChain()
    {
        // Test de notre chaine de responsabilité
        // Arrange
        TestWave wave1 = new TestWave();
        TestWave wave2 = new TestWave();
        TestWave wave3 = new TestWave();
        TestWave wave4 = new TestWave();

        // Act
        wave1.SetNext(wave2);
        wave2.SetNext(wave3);
        wave3.SetNext(wave4);
        wave4.SetNext(wave1);

        // Assert
        Assert.AreEqual(wave2, wave1.GetNextWave());
        Assert.AreEqual(wave3, wave2.GetNextWave());
        Assert.AreEqual(wave4, wave3.GetNextWave());
        Assert.AreEqual(wave1, wave4.GetNextWave());
    }

    [Test]
    public void WaveCompletion_ShouldTriggerOnWaveCompleted()
    {
        wave.GenerateWave(testTargetPosition);
        wave.SimulateWaveCompletion();

        Assert.IsTrue(wave.WaveCompletedCalled);
    }
}