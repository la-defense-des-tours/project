using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class IceEffect : TowerDecorator
    {
        public IceEffect(Tower tower) : base(tower)
        {
            Debug.Log("Ice Effect Applied");
            effectType = "Ice";
        }

        public override void Attack()
        {
            base.Attack();
            Debug.Log("Ice effect is attacking");
            if (m_shooter != null)
            {
                m_shooter.SetEffectType(effectType);
            }
            else
            {
                Debug.LogError("m_shooter is null in IceEffect");
            }
        }
    }
}