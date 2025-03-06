using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class TowerDecorator : Tower
    {
        private Tower tower;
        public override string effectType => tower.effectType;

        protected TowerDecorator(Tower tower)
        {
            this.tower = tower;
        }

        public void ApplyEffect()
        {
            this.tower.effectType = effectType;
            this.tower.InitialiseBullet(effectType);
        }
    }
}