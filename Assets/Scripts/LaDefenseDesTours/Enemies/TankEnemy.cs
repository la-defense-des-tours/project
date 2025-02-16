using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class TankEnemy : Enemy
    {
        public override float maxHealth { get; set; } = 350;
        public override float speed { get; set; } = 1.5f;
        public override float acceleration { get; set; } = 2.5f;

    }
}