using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class FireEffect : TowerDecorator
    {
        public FireEffect(Tower tower) : base(tower)
        {
            Debug.Log("Fire Effect Applied");
            effectType = "Fire";
        }

        public override void Attack()
        {
            base.Attack();
            Debug.Log("Fire effect is attacking");
            // if (m_shooter != null)
            // {
            //     m_shooter.SetEffectType(effectType);
            // }
            // else
            // {
            //     Debug.LogError("No shooter component found!");
            // }

        }
    }
}