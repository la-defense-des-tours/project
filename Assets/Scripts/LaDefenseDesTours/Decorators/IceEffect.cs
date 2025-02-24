using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class IceEffect : TowerDecorator
    {
        public IceEffect(Tower tower) : base(tower)
        {
            Debug.Log("Ice Effect Applied");
        }

        public override void Attack()
        {
            base.Attack();
            Debug.Log("Ice effect is attacking");
            m_shooter.SetEffectType("Ice");
        }
    }
}