using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public sealed class LightningEffect : TowerDecorator
    {
        public LightningEffect(Tower tower) : base(tower)
        {
            tower.effectType = "Lightning";
        }

        protected override float DamageModifier()
        {
            return 0.5f;
        }
    }
}