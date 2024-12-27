using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class IceEffect : TowerDecorator
    {
        public IceEffect(Tower tower) : base(tower) { }

        public override void Attack()
        {
            base.Attack();
            Debug.Log("Ice effect is attacking");
        }
        public override void Upgrade()
        {
            tower.Upgrade();
        }
    }
}
