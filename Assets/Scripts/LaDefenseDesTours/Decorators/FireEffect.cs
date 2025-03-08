using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public sealed class FireEffect : TowerDecorator
    {
        public FireEffect(Tower tower) : base(tower)
        {
            tower.effectType = "Fire";
        }

        protected override float DamageModifier()
        {
            return 1.25f;
        }
    }
}