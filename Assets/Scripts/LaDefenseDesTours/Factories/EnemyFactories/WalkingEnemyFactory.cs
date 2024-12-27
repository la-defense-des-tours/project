using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class WalkingEnemyFactory : MonoBehaviour, EnemyFactory
    {
        [SerializeField] private WalkingEnemy walkingEnemy;
        public void Start()
        {
            CreateEnemy();
        }
        public Enemy CreateEnemy()
        {
            Notify();
            GameObject instance = Instantiate(walkingEnemy.gameObject);
            return instance.GetComponent<WalkingEnemy>();
        }
        public void Notify()
        {
            Debug.Log("Walking enemy created!");
        }
    }
}