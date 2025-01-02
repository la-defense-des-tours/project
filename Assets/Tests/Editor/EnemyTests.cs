using Assets.Scripts.LaDefenseDesTours.Interfaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTests
{
    private class TestEnemy : MonoBehaviour, Enemy
    {
        public bool isDead = false;
        public float health = 100f;

        public void Die()
        {
            isDead = true;
            DestroyImmediate(gameObject);
        }
        public void SetupNavMeshAgent()
        {
            // Implement interface method
        }
        public void Move(Vector3 destination)
        {
            // Implement interface method
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
        Assert.AreEqual(90f, enemy.health);
    }

    [Test]
    public void HealthBelowZero_CallsDie()
    {
        enemy.TakeDamage(110f);
        Assert.IsTrue(enemy.isDead);
    }

    [Test]
    public void HealthZero_CallsDie()
    {
        enemy.TakeDamage(100f);
        Assert.IsTrue(enemy.isDead);
    }
}