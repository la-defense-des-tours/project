using Assets.Scripts.LaDefenseDesTours.Interfaces;
using LaDefenseDesTours.Enemies;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTests
{
    private class TestEnemy : Enemy
    {
        public bool isDead = false;
        public TestEnemy()
        {
            health = 100;
            speed = 5;
            acceleration = 5;
        }

        public override void Die()
        {
            isDead = true;
            DestroyImmediate(gameObject);
        }
        protected override void SetupNavMeshAgent()
        {
        }
        public override void Move(Vector3 destination)
        {
        }
        protected override void CheckArrival()
        {
        }
        public override void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
                Die();
        }
        public override float GetSpeed()
        {
            return speed;
        }
        public override void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }
    }

    private TestEnemy enemy;

    [SetUp]
    public void SetUp()
    {
        GameObject enemyObject = new("Enemy");
        enemyObject.AddComponent<NavMeshAgent>();
        enemy = enemyObject.AddComponent<TestEnemy>();
    }

    [TearDown]
    public void TearDown()
    {
        if (enemy != null && enemy.gameObject != null)
            Object.DestroyImmediate(enemy.gameObject);
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
        double expectedPlayerHealth = 1900;
        double resultPlayerHealth = Player.GetInstance().health;

        Assert.AreEqual(expectedPlayerHealth, resultPlayerHealth);
    }

    [Test]
    public void DealDamage_CallsPlayerTakeDamageWithCorrectValue()
    {
        enemy.DealDamage(200f);
        double expectedPlayerHealth = 1700;
        double resultPlayerHealth = Player.GetInstance().health;

        Assert.AreEqual(expectedPlayerHealth, resultPlayerHealth);
    }

}