using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class TowerDecorator : Tower
    {
        protected Tower tower;
        public override string towerName => tower.towerName;
        public override float range => tower.range;
        public override float damage => tower.damage;

        public TowerDecorator(Tower tower)
        {
            this.tower = tower;
        }

        public void SetTower(Tower tower)
        {
            this.tower = tower;
        }

        public override void Attack()
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

        public override void Upgrade()
        {
            if (tower != null)
            {
                tower.Upgrade();
            }
            else
            {
                Debug.Log("No tower to upgrade");
            }
        }
    }
}