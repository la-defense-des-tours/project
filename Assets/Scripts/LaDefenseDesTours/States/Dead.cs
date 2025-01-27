using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public class Dead : State
    {
        public override void ApplyEffect()
        {
            if (enemy == null)
                return;

            enemy.Die();
        }
    }
}