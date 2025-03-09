using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public sealed class IceEffect : TowerDecorator
    {
        public IceEffect(Tower tower) : base(tower)
        {
            tower.effectType = "Ice";
        }

        protected override float DamageModifier()
        {
            return 0.75f;
        }
    }
}