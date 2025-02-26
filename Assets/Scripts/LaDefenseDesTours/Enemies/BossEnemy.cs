using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class BossEnemy : Enemy
    {
        public BossEnemy()
        {
            maxHealth = Mathf.Infinity;
            speed = 1;
            acceleration = 3;
        }
    }
}