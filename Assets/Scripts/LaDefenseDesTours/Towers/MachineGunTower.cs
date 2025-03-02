using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class MachineGunTower : Tower
    {
        public override string towerName { get; } = "Machine Gun Tower";
        public float attackPerSecond { get; set; } = 1.5f;

        public MachineGunTower()
        {
            currentLevel = 1;
            range = 30f;
            damage = 30f;
            cost = 80;
        }
        public override void Upgrade()
        {
            currentLevel++;

            switch (currentLevel)
            {
                case 2:
                    cost += 40;
                    damage += 40f;
                    range += 5f;
                    attackPerSecond += 0.5f;
                    break;

                case 3:
                    cost += 80;
                    damage += 60f;
                    range += 10f;
                    attackPerSecond += 1f;
                    break;

                default:
                    Debug.LogError("Max upgrade level reached!");
                    break;
            }
        }
    }
}
