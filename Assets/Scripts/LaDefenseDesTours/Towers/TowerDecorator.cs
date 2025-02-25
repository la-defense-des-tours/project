using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class TowerDecorator : Tower
    {
        protected Tower tower;
        protected string effectType;
        public override string towerName => tower.towerName;
        public override float range => tower.range;
        public override float damage => tower.damage;

        public TowerDecorator(Tower tower)
        {
            this.tower = tower;
            m_shooter = tower.GetShooter();
        }

        public void SetTower(Tower tower)
        {
            this.tower = tower;
            m_shooter = tower.GetShooter();
        }

        public override void Attack()
        {
            tower.Attack();
            m_shooter.SetEffectType(effectType);
        }
    }
}