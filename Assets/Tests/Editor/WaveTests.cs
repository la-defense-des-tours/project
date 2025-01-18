using NUnit.Framework;
using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using System.Collections.Generic;

public class TestWave : BaseWave
{
    public TestWave GetNextWave() => (TestWave)nextWave;
    public Vector3 GetTargetPosition() => targetPosition;

    public override void SpawnEnemies(Vector3 targetPosition)
    {
        Debug.Log("Spawning enemies...");
    }
}

public class WaveTests
{
    private TestWave wave;
    private TestWave nextWave;

    [SetUp]
    public void SetUp()
    {
        wave = new TestWave();
        nextWave = new TestWave();
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
        Vector3 targetPosition = new Vector3(1, 2, 3);

        wave.GenerateWave(targetPosition);
        Assert.AreEqual(targetPosition, wave.GetTargetPosition());
    }
}