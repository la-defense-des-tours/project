using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class Tower : MonoBehaviour
    {
        public virtual  string towerName { get; }
        public virtual float range { get; }
        protected int currentLevel;
        public virtual float damage { get; }
        protected float fireRate;
        public virtual int cost { get; set; }
        protected int health;
        protected int upgradeCost;
        protected float upgradeDamage;
        protected float upgradeFireRate;
        protected float upgradeRange;
        protected int sellValue;
        protected int upgradeSellValue;

        public virtual void Upgrade()
        {
            Debug.Log("Upgrading base tower...");
            currentLevel++;
            health += 10;
        }

        public virtual void Attack()
        {
            Debug.Log("Base tower attack");
        }

        public virtual void SetPosition(Vector3 position)
        {
            position.x = Mathf.Round(position.x);
            position.z = Mathf.Round(position.z);
            transform.position = position;
        }
    }
}