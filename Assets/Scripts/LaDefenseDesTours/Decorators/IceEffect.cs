using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class IceEffect : TowerDecorator
    {
        public IceEffect(Tower tower) : base(tower)
        {
            effectType = "Ice";
        }
    }
}