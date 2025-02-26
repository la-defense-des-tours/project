using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class LightningEffect : TowerDecorator
    {
        public LightningEffect(Tower tower) : base(tower)
        {
            effectType = "Lightning";
        }
    }
}