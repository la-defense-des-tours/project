using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;


namespace Assets.Scripts.LaDefenseDesTours.Enemies { 
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyFactory walkingEnemyFactory;
        [SerializeField] private EnemyFactory flyingEnemyFactory;
        [SerializeField] private EnemyFactory tankEnemyFactory;
        [SerializeField] private Transform target;
        private Enemy walkingEnemy;
        private Enemy flyingEnemy;
        private Enemy tankEnemy;

        public void Start()
        {
            SpawnEnemy();
        }
        public void SpawnEnemy()
        {
            walkingEnemy = walkingEnemyFactory.CreateEnemy();
            flyingEnemy = flyingEnemyFactory.CreateEnemy();
            tankEnemy = tankEnemyFactory.CreateEnemy();

            walkingEnemy.Move(target.position);
            flyingEnemy.Move(target.position);
            tankEnemy.Move(target.position);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Enemy walkingClone = walkingEnemy.Clone();
                Enemy flyingClone = flyingEnemy.Clone();
                Enemy tankClone = tankEnemy.Clone();

                walkingClone.Move(target.position);
                flyingClone.Move(target.position);
                tankClone.Move(target.position);
            }
        }
    }
}