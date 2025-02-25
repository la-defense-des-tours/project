
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

        protected float fireRate;
        protected int upgradeCost;
        protected float upgradeDamage;
        protected float upgradeFireRate;
        protected float upgradeRange;
        protected int sellValue;

        public virtual void Start()
        {
            m_shooter = GetComponent<Shooter>();
            m_shooter.SetRange(range);
            m_shooter.SetDamage(damage);
        }

        public virtual void Update()
        {
            switch (Input.inputString)
            {
                case "i":
                    var iceEffect = new IceEffect(this);
                    AttachEffect(iceEffect);
                    Debug.Log("Ice effect attached");
                    iceEffect.Attack();
                    break;
                case "f":
                    var fireEffect = new FireEffect(this);
                    AttachEffect(fireEffect);
                    Debug.Log("Fire effect attached");
                    fireEffect.Attack();
                    break;
                case "l":
                    AttachEffect(new LightningEffect(this));
                    Debug.Log("Lightning effect attached");
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

        public Tower AttachEffect(TowerDecorator effect)
        {
            effect.SetTower(this);
            return effect;
        }
    }
}
