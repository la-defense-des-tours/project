
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class Tower : MonoBehaviour
    {
        protected Shooter m_shooter;
        public virtual string towerName { get; }
        public virtual float range { get; set; }
        public virtual float damage { get; set; }
        public virtual int cost { get; set; }
        public int currentLevel { get; set; } = 1;
        public bool isGhost { get; set; } = false;

        protected float fireRate;
        protected int upgradeCost;
        protected float upgradeDamage;
        protected float upgradeFireRate;
        protected float upgradeRange;
        protected int sellValue;


        public virtual void Start()
        {
            if (isGhost) return;

            m_shooter = GetComponent<Shooter>();
            m_shooter.SetRange(range);
            m_shooter.SetDamage(damage);
        }

        public Shooter GetShooter()
        {
            return m_shooter;
        }

        public virtual void Update()
        {
            switch (Input.inputString)
            {
                case "i":
                    var iceEffect = new IceEffect(this);
                    iceEffect.Attack();
                    break;
                case "f":
                    var fireEffect = new FireEffect(this);
                    fireEffect.Attack();
                    break;
                case "l":
                    var lightningEffect = new LightningEffect(this);
                    lightningEffect.Attack();
                    break;
            }
        }

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
