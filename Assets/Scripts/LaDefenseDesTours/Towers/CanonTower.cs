using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class CanonTower : Tower
    {
        internal GameObject gameObject;
        public float areaOfEffect { get; set; }
        public override void Upgrade()
        {
            level++;
            health += 15;
            price += 25;
            areaOfEffect += 0.5f;
        }
        public override void Attack()
        {
            Debug.Log("Canon Tower is attacking");
        }
    }

}
