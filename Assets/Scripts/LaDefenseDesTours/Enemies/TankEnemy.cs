using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
namespace Assets.Scripts.LaDefenseDesTours.Enemies
{
    public class TankEnemy : Enemy
    {

        TankEnemy()
        {
            maxHealth = 350f;
            speed = 1.5f;
            acceleration = 2.5f;
        }

    }
}