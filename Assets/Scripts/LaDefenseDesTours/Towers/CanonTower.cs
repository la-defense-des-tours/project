using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class CanonTower : Tower
    {
        public override string towerName { get; } = "Canon Tower";
        public float areaOfEffect { get; set; }
        public override float range { get;set; } = 20f;
        public override float damage { get; set;  } = 100f;

        public override void Upgrade()
        {
            currentLevel++;

            switch (currentLevel)
            {
                case 1:
                    // 1er upgrade
                    health += 20;
                    cost += 25;
                    areaOfEffect += 0.5f;
                    damage += 15f;
                    range += 5f;
                    break;

                case 2:
                    // 2eme upgrade
                    health += 30;
                    cost += 50;
                    areaOfEffect += 1.5f;
                    damage += 25f;
                    range += 5f;
                    break;

                default:
                    Debug.LogError("Max upgrade level reached!");
                    break;
            }

            Debug.Log($"Canon Tower upgraded to level {currentLevel}. New Stats - Health: {health}, Damage: {damage}, Range: {range}, Cost: {cost}, Area of effect: {areaOfEffect}");
        }

        public override void Attack()
        {
            Debug.Log("Canon Tower is attacking");
        }
    }

}
