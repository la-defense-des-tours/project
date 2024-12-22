// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.AI;

// public class EnemyTests
// {
//     private class TestEnemy : Enemy
//     {
//         public bool isDead = false;

//         public float Health
//         {
//             get { return health; }
//             set { health = value; }
//         }

//         public override void Move()
//         {
//             // Implement abstract method
//         }

//         public override void Die()
//         {
//             isDead = true;
//             base.Die();
//         }
//     }

//     private TestEnemy enemy;

//     [SetUp]
//     public void SetUp()
//     {
//         GameObject enemyObject = new GameObject();
//         enemyObject.AddComponent<NavMeshAgent>();
//         enemy = enemyObject.AddComponent<TestEnemy>();
//         enemy.Health = 100f;
//     }

//     [Test]
//     public void TakeDamage_ReducesHealth()
//     {
//         enemy.TakeDamage(10f);
//         Assert.AreEqual(90f, enemy.Health);
//     }

//     [Test]
//     public void HealthBelowZero_CallsDie()
//     {
//         enemy.TakeDamage(110f);
//         Assert.IsTrue(enemy.isDead);
//     }

//     [Test]
//     public void HealthZero_CallsDie()
//     {
//         enemy.TakeDamage(100f);
//         Assert.IsTrue(enemy.isDead);
//     }
// }