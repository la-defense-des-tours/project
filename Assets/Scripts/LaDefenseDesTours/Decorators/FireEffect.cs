using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class FireEffect : TowerDecorator
    {
        public FireEffect(Tower tower) : base(tower) { }

        public override void Attack()
        {
            base.Attack();
            Debug.Log("Fire effect is attacking");
        }

        public override void Upgrade()
        {
            tower.Upgrade();
        }
    }
}
