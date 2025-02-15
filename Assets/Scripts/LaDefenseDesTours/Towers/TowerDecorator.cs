using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class TowerDecorator : Tower
    {
        protected Tower tower;
        public string towerName => tower.towerName;
        public float range => tower.range;
        public float damage => tower.damage;

        public TowerDecorator(Tower tower)
        {
            this.tower = tower;
        }

        public void SetTower(Tower tower)
        {
            this.tower = tower;
        }

        public void Attack()
        {
            if (tower != null)
            {
                tower.Attack();
            }
            else
            {
                Debug.Log("No tower to attack");
            }
        }

        public void Upgrade()
        {
            Debug.Log("Upgrading tower");
        }

        public void SetupNavMeshAgent()
        {
            throw new System.NotImplementedException();
        }

        public void SetPosition(Vector3 position)
        {
            throw new System.NotImplementedException();
        }
        public float GetTowerRange()
        {
            throw new System.NotImplementedException();
        }
        public float GetTowerDamage()
        {
            throw new System.NotImplementedException();
        }
    }
}