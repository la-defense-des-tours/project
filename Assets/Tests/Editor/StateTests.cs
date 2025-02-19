using Assets.Scripts.LaDefenseDesTours.Interfaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

public class StateTests
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

        public override void SetupNavMeshAgent()
        {
        }

        public override void Move(Vector3 destination)
        {
        }

        public override void CheckArrival()
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
    private GameObject enemyObject;

    [SetUp]
    public void SetUp()
    {
        enemyObject = new GameObject("Enemy");
        enemyObject.AddComponent<NavMeshAgent>();

        CreateParticleSystem("SlowedEffect");
        CreateParticleSystem("ParalyzedEffect");
        CreateParticleSystem("BurnedEffect");
        CreateParticleSystem("DeadEffect");

        enemy = enemyObject.AddComponent<TestEnemy>();
    }

    private void CreateParticleSystem(string name)
    {
        GameObject effectObject = new GameObject(name);
        effectObject.transform.parent = enemyObject.transform;
        effectObject.AddComponent<ParticleSystem>();
    }

    [TearDown]
    public void TearDown()
    {
        if (enemyObject != null)
            Object.DestroyImmediate(enemyObject);
    }

    [Test]
    public void Slowed_ReducesSpeed()
    {
        Slowed slowedState = new Slowed();
        slowedState.SetContext(enemy);

        float initialSpeed = enemy.GetSpeed();
        float slowFactor = 0.5f;
        slowedState.ApplyEffect();

        float expected = initialSpeed * slowFactor;
        float result = enemy.GetSpeed();
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void Paralyzed_StopsEnemy()
    {
        Paralyzed paralyzedState = new Paralyzed();
        paralyzedState.SetContext(enemy);

        paralyzedState.ApplyEffect();

        Assert.AreEqual(0, enemy.GetSpeed());
    }

    [Test]
    public void Burned_DealsDamageOverTime()
    {
        Burned burnedState = new Burned();
        burnedState.SetContext(enemy);

        float initialHealth = enemy.health;
        burnedState.ApplyEffect();

        Assert.Less(enemy.health, initialHealth);
    }

    [Test]
    public void Dead_DisablesEnemyAndDestroysAfterDelay()
    {
        Dead deadState = new Dead();
        deadState.SetContext(enemy);

        deadState.OnStateEnter();
        Assert.AreEqual("Untagged", enemy.gameObject.tag);

        // Test that renderers are disabled
        MeshRenderer[] renderers = enemy.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in renderers)
        {
            Assert.IsFalse(r.enabled);
        }
    }

    [Test]
    public void State_ParticleEffects_PlayAndStop()
    {
        Slowed slowedState = new Slowed();
        slowedState.SetContext(enemy);

        ParticleSystem ps = enemy.transform.Find("SlowedEffect").GetComponent<ParticleSystem>();
        Assert.NotNull(ps);

        slowedState.OnStateEnter();
        Assert.IsTrue(ps.isPlaying);

        slowedState.OnStateExit();
        Assert.IsFalse(ps.isPlaying);
    }
}