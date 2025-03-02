
using Assets.Scripts.LaDefenseDesTours.Towers.Data;
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
        protected virtual float specialAbility { get; set; }
        public virtual string effectType { get; set; }
        public virtual int cost { get; set; }
        public int currentLevel { get; set; } = 1;

        public bool isAtMaxLevel { get; set; } = false;
        public bool isGhost { get; set; } = false;

        public TowerData towerData;

        public virtual void Start()
        {
            if (isGhost) return;

            m_shooter = GetComponent<Shooter>();
            
            if (m_shooter != null)
                m_shooter.Initialize(range, damage, specialAbility, effectType);
        }

        public virtual void Update()
        {
            switch (Input.inputString)
            {
                case "i":
                    ApplyEffect(new IceEffect(this));
                    break;
                case "f":
                    ApplyEffect(new FireEffect(this));
                    break;
                case "l":
                    ApplyEffect(new LightningEffect(this));
                    break;
            }
        }

        public virtual void Upgrade()
        {
            currentLevel++;
        }

        public void ApplyEffect(TowerDecorator decorator)
        {
            effectType = decorator.effectType;
            m_shooter.Initialize(range, damage, specialAbility, effectType);
        }
    }
}
