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
    }
}