using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class CanonTower : Tower
    {
        public float areaOfEffect { get; set; } = 5f;

        public CanonTower()
        {
            specialAbility = areaOfEffect;
        }
        public override void Start()
        {
            if (isGhost) return;
            base.Start();
        }
    
    }

}
