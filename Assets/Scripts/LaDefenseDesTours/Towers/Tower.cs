using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class Tower : MonoBehaviour
    {
        public virtual string towerName { get; }
        public virtual float range { get; set; }
        public virtual float damage { get; set; }
        public virtual int cost { get; set; }
        public  int currentLevel;
        protected float fireRate;
        protected int upgradeCost;
        protected float upgradeDamage;
        protected float upgradeFireRate;
        protected float upgradeRange;
        protected int sellValue;

        public virtual void Upgrade()
        {
            Debug.Log("Upgrading base tower...");
            currentLevel++;
        }

        public virtual void Attack()
        {
            Debug.Log("Base tower attack");
        }
    }
}