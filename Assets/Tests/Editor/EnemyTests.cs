using Assets.Scripts.LaDefenseDesTours.Interfaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTests
{
    private class TestEnemy : Enemy
    {
        public bool isDead = false;
        public override float health { get; set; } = 100f;
        public override float speed { get; set; }  = 5f;

        public override void Die()
        {
            isDead = true;
            DestroyImmediate(gameObject);
        }
        public override void SetupNavMeshAgent()
        {
        }
        public override void Move(Vector3 destination)
        {
        }
     
       
        public override void CheckArrival()
        {
        }
    }

    private TestEnemy enemy;

    [SetUp]
    public void SetUp()
    {
        GameObject enemyObject = new GameObject("Enemy");
        enemyObject.AddComponent<NavMeshAgent>();
        enemy = enemyObject.AddComponent<TestEnemy>();
    }

    [Test]
    public void TakeDamage_ReducesHealth()
    {
        enemy.TakeDamage(10f);
        float expected = 90f;
        float result = enemy.health;
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void HealthBelowZero_CallsDie()
    {
        enemy.TakeDamage(110f);
        bool expected = true;
        bool result = enemy.isDead;
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void HealthZero_CallsDie()
    {
        enemy.TakeDamage(100f);
        bool expected = true;
        bool result = enemy.isDead;
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TransitionTo_SlowsEnemy()
    {
        enemy.TransitionTo(new Slowed());
        Assert.Pass();
    }

    [Test]
    public void GetSpeed_ReturnsSpeed()
    {
        float expected = 5f;
        float result = enemy.GetSpeed();
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void SetSpeed_ChangesSpeed()
    {
        enemy.SetSpeed(10f);
        float expected = 10f;
        float result = enemy.GetSpeed();
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void DealDamage_CallsPlayerTakeDamage()
    {
        enemy.DealDamage(100f);
        double expectedPlayerHealth = 900;
        double resultPlayerHealth = Player.GetInstance().health;

        Assert.AreEqual(expectedPlayerHealth, resultPlayerHealth);
    }

    [Test]
    public void DealDamage_CallsPlayerTakeDamageWithCorrectValue()
    {
        enemy.DealDamage(200f);
        double expectedPlayerHealth = 700;
        double resultPlayerHealth = Player.GetInstance().health;

        Assert.AreEqual(expectedPlayerHealth, resultPlayerHealth);
    }

}