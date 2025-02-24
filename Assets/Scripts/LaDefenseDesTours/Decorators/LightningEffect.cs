using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class LightningEffect : TowerDecorator
    {
        public LightningEffect(Tower tower) : base(tower)
        {
            Debug.Log("Lightning Effect Applied");
        }

        public override void Attack()
        {
            base.Attack();
            Debug.Log("Lightning effect is attacking");
            m_shooter.SetEffectType("Lightning");
        }
    }
}