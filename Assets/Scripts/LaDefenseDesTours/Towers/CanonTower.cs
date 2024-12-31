using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class CanonTower : MonoBehaviour, Tower
    {
        private NavMeshAgent agent;
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
        public float areaOfEffect { get; set; }
        public void Upgrade()
        {
            currentLevel++;
            health += 15;
            cost += 25;
            areaOfEffect += 0.5f;
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
            Debug.Log("Canon Tower is attacking");
        }
        void Tower.SetPosition(Vector3 position)
        {
            position.x = Mathf.Round(position.x);
            position.z = Mathf.Round(position.z);
            transform.position = position;
        }
    }

}
