using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class CanonTower : Tower
    {
        private float areaOfEffect { get; set; } = 5f;

        public CanonTower()
        {
            specialAbility = areaOfEffect;
        }
    }

}
