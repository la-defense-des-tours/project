using UnityEngine;
using Assets.Scripts.LaDefenseDesTours.Interfaces;

namespace Assets.Scripts.LaDefenseDesTours.Towers
{
    public class CanonTower : Tower
    {
        public override string towerName { get; } = "Canon Tower";
        public float areaOfEffect { get; set; } = 5f;

        public CanonTower()
        {
            currentLevel = 1; 
            range = 20f;
            damage = 100f;
            cost = 120;
            specialAbility = areaOfEffect;
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
                    cost += 25;
                    areaOfEffect += 5f;
                    damage += 15f;
                    range += 5f;
                    break;

                case 3:
                    cost += 50;
                    areaOfEffect += 10f;
                    damage += 25f;
                    range += 5f;
                    break;

                default:
                    Debug.LogError("Max upgrade level reached!");
                    break;
            }
        }
    }

}
