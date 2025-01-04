using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Core
{ 
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyFactory walkingEnemyFactory;
        [SerializeField] private EnemyFactory flyingEnemyFactory;
        [SerializeField] private EnemyFactory tankEnemyFactory;
        [SerializeField] private EnemyFactory bossEnemyFactory;
        [SerializeField] private Transform target;
        private Enemy walkingEnemy;
        private Enemy flyingEnemy;
        private Enemy tankEnemy;
        private Enemy bossEnemy;

        public void Start()
        {
            SpawnEnemy();
        }
        public void SpawnEnemy()
        {
            walkingEnemy = walkingEnemyFactory.CreateEnemy();
            flyingEnemy = flyingEnemyFactory.CreateEnemy();
            tankEnemy = tankEnemyFactory.CreateEnemy();
            bossEnemy = bossEnemyFactory.CreateEnemy();

            walkingEnemy.Move(target.position);
            flyingEnemy.Move(target.position);
            tankEnemy.Move(target.position);
            bossEnemy.Move(target.position);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Enemy walkingClone = walkingEnemy.Clone(transform);
                Enemy flyingClone = flyingEnemy.Clone(transform);
                Enemy tankClone = tankEnemy.Clone(transform);
                Enemy bossClone = bossEnemy.Clone(transform);

                walkingClone.Move(target.position);
                flyingClone.Move(target.position);
                tankClone.Move(target.position);
                bossClone.Move(target.position);
            }
        }
    }
}