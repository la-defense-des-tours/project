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
            float damageModifier = DamageModifier();
            float damage = tower.towerData.dps * damageModifier;

            this.tower.effectType = effectType;
            this.tower.InitialiseBullet(effectType, damage);
            this.tower.ChangeMaterial(effectType);
        }

        protected abstract float DamageModifier();
    }
}