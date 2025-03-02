using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class LaserTower : Tower
    {
        public override string towerName { get; } = "Laser Tower";
        public float damageOverTime { get; set; } = 1f;

        public LaserTower()
        {
            currentLevel = 1;
            range = 50f;
            damage = 25f;
            cost = 100;
            specialAbility = damageOverTime;
        }
        public override void Start()
        {
            if (isGhost) return;
            base.Start();
        }
        public override void Upgrade()
        {
            currentLevel++;

            switch (currentLevel)
            {
                case 2:
                    cost += 50;
                    damageOverTime += 1f;
                    damage += 5f;
                    range += 5f;
                    break;

                case 3:
                    cost += 100;
                    damageOverTime += 2f;
                    damage += 10f;
                    range += 10f;
                    isAtMaxLevel = true;
                    break;

                default:
                    Debug.LogError("Max upgrade level reached!");
                    break;
            }
        }
    }
}
