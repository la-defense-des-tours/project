using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class IceEffect : TowerDecorator
    {
        public IceEffect(Tower tower) : base(tower) { }

        public new void Attack()
        {
            base.Attack();
            Debug.Log("Ice effect is attacking");
        }
    }
}
