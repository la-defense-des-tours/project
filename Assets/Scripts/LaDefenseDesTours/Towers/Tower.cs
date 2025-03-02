
using Assets.Scripts.LaDefenseDesTours.Towers.Data;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class Tower : MonoBehaviour
    {
        protected Shooter m_shooter;
        public virtual float range { get; set; }
        public virtual float damage { get; set; }
        protected virtual float specialAbility { get; set; }
        public virtual string effectType { get; set; }
        public int currentLevel { get; set; } = 1;
        public bool isAtMaxLevel { get; set; } = false;
        public bool isGhost { get; set; } = false;

        public TowerData towerData;

        [SerializeField] public GameObject towerPrefabs;

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
                    new IceEffect(this);
                    break;
                case "f":
                    new FireEffect(this);
                    break;
                case "l":
                    new LightningEffect(this);
                    break;
                case "n":
                    Upgrade();
                    break;

            }
        }

        public virtual void Sell()
        {
            Destroy(gameObject);
        }

        public virtual void Upgrade()
        {
            if (isAtMaxLevel) return;

            currentLevel++;

            if (currentLevel >= 3)
            {
                isAtMaxLevel = true;
                return;
            }

            GameObject nextPrefab = towerPrefabs;

            if (nextPrefab == null)
            {
                return;
            }
            Cell parentCell = GetComponentInParent<Cell>();
            if (parentCell == null)
            {
                Debug.LogError("Impossible de trouver la cellule parente !");
                return;
            }

            GameObject newTowerObj = Instantiate(nextPrefab, transform.position, transform.rotation);
            Tower newTower = newTowerObj.GetComponent<Tower>();

            newTower.transform.SetParent(parentCell.transform);

            Destroy(gameObject);
        }



        public void InitialiseBullet(string effect)
        {
            m_shooter.Initialize(range, damage, specialAbility, effect);
        }
    }
}
