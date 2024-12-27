using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class LightningEffect : TowerDecorator
    {
        public LightningEffect(Tower tower) : base(tower) { }

        public override void Attack()
        {
            base.Attack();
            Debug.Log("Lightning effect is attacking");
        }
        public override void Upgrade()
        {
            tower.Upgrade();
        }
    }
}
