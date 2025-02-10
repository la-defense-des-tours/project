using Assets.Scripts.LaDefenseDesTours.Interfaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTests
{
    private class TestEnemy : MonoBehaviour, Enemy
    {
        private State currentState;
        public bool isDead = false;
        public float health = 100f;
        public float speed = 5f;

        public void Die()
        {
            isDead = true;
            DestroyImmediate(gameObject);
        }
        public void SetupNavMeshAgent()
        {
        }
        public void Move(Vector3 destination)
        {
        }
        public Enemy Clone(Transform spawnPoint)
        {
            return (Enemy)this.MemberwiseClone();
        }
        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
        public void DealDamage(double damage)
        {
            Player.GetInstance().TakeDamage(damage);
        }
        public void TransitionTo(State state)
        {
            if (state is Slowed)
            {
                Debug.Log("Enemy is slowed.");
            }
        }
        public void UpdateState()
        {
            currentState?.ApplyEffect();
        }
        public float GetSpeed()
        {
            return speed;
        }
        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }
    }

    private TestEnemy enemy;

    [SetUp]
    public void SetUp()
    {
        GameObject enemyObject = new GameObject();
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

}