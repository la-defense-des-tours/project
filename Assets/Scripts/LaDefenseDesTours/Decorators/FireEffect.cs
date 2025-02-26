using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class FireEffect : TowerDecorator
    {
        public FireEffect(Tower tower) : base(tower)
        {
            effectType = "Fire";
        }
    }
}