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

        public override void Start()
        {
            base.Start();
            m_shooter = GetComponent<Shooter>();
            Debug.Log("Is here m shooter : " + m_shooter);
            // m_shooter.SetEffectType(effectType);
        }

        public TowerDecorator(Tower tower)
        {
            this.tower = tower;
            // this.m_shooter = tower.m_shooter;
        }

        public void SetTower(Tower tower)
        {
            this.tower = tower;
            // this.m_shooter = tower.m_shooter;
        }

        public override void Attack()
        {
            tower.Attack();
            if (m_shooter != null)
            {
                m_shooter.SetEffectType(effectType);
            }
            else
            {
                Debug.LogError("No shooter component found!");
            }
        }
    }
}