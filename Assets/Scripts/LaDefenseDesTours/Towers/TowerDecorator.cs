using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class TowerDecorator : Tower
    {
        private Tower tower;
        public override string towerName => tower.towerName;
        public override float range => tower.range;
        public override float damage => tower.damage;
        public override string effectType => tower.effectType;

        protected TowerDecorator(Tower tower)
        {
            this.tower = tower;
        }
    }
}