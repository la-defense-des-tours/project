using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class TowerDecorator : Tower
    {
        protected Tower tower;

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

        public Tower Clone()
        {
            throw new System.NotImplementedException();
        }
    }
}