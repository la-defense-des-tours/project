using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Normal : State
    {
        public override void ApplyEffect()
        {
            enemy.SetupSpeed();
        }
    }
}