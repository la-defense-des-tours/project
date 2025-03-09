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

            tower.effectType = effectType;
            tower.InitialiseBullet(effectType, damage);
            tower.ChangeMaterial(effectType);
        }

        protected abstract float DamageModifier();
    }
}