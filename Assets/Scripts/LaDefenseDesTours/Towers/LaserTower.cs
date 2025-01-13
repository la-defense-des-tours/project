using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserTower : MonoBehaviour, Tower
    {
        private NavMeshAgent agent;

        public string towerName { get; set; } = "Laser Tower";
        private float range { get; set; }
        private int currentLevel { get; set; }
        private float damage { get; set; }
        private float fireRate { get; set; }
        private int cost { get; set; }
        private int health { get; set; }
        private int upgradeCost { get; set; }
        private float upgradeDamage { get; set; }
        private float upgradeFireRate { get; set; }
        private float upgradeRange { get; set; }
        private int sellValue { get; set; }
        private int upgradeSellValue { get; set; }
        public float damageOverTime { get; set; }
        public void Upgrade()
        {
            currentLevel++;
            health += 50;
            cost += 50;
            damageOverTime += 1.5f;
        }
        //     public void Move(Vector3 destination)
        // {
        //     agent.SetDestination(destination);
        // }
        public void SetupNavMeshAgent()
        {
            if (gameObject.GetComponent<NavMeshAgent>() == null)
            {
                agent = gameObject.AddComponent<NavMeshAgent>();
            }
            else
            {
                agent = gameObject.GetComponent<NavMeshAgent>();
            }
        }
        public void Attack()
        {
            Debug.Log("Laser Tower is attacking");
        }

        void Tower.SetPosition(Vector3 position)
        {
            position.x = Mathf.Round(position.x);
            position.z = Mathf.Round(position.z);
            transform.position = position;
        }
    }
}
