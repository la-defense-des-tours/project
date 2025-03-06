using Assets.Scripts.LaDefenseDesTours.Towers.Data;
using System.Collections.Generic;
using LaDefenseDesTours.Strategy;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    public abstract class Tower : MonoBehaviour
    {
        private Shooter m_shooter;
        protected virtual float specialAbility { get; set; }
        public virtual string effectType { get; set; }
        public bool isAtMaxLevel { get; set; } = false;
        public bool isGhost { get; set; } = false;
        public int firePrice { get; set; } = 5000;
        public int icePrice { get; set; } = 5000;
        public int lightPrice { get; set; } = 15000;

        private Renderer[] renderers;
        private Color defaultColor;
        private readonly Color hoverColor = new(0f, 0.8f, 0.8f, 0.35f);
        public TowerData towerData;
        [SerializeField] public GameObject towerPrefabs;
        private IStrategy strategy;

        public virtual void Start()
        {
            if (isGhost) return;

            strategy = new NearestEnemy();

            m_shooter = GetComponent<Shooter>();
            if (m_shooter != null)
            {
                m_shooter.Initialize(towerData.range, towerData.dps, specialAbility, effectType, strategy, towerData.fireRate);
            }

            renderers = GetComponentsInChildren<Renderer>();
            if (renderers.Length > 0 && renderers[0].material.HasProperty("_Color"))
            {
                defaultColor = renderers[0].material.color;
            }
        }

        public virtual void Update()
        {
            TestDecorators();
        }

        public void Sell()
        {
            Destroy(gameObject);
        }

        public void Upgrade()
        {
            if (isAtMaxLevel) return;

            GameObject nextPrefab = towerPrefabs;

            if (nextPrefab == null) return;

            Cell parentCell = GetComponentInParent<Cell>();
            if (parentCell == null) return;

            GameObject newTowerObj = Instantiate(nextPrefab, transform.position, transform.rotation);
            Tower newTower = newTowerObj.GetComponent<Tower>();
            newTower.transform.SetParent(parentCell.transform);

            if (newTower.towerData.currentLevel >= 3)
            {
                newTower.isAtMaxLevel = true;
            }

            Destroy(gameObject);
        }

        public void InitialiseBullet(string effect)
        {
            m_shooter.Initialize(towerData.range, towerData.dps, specialAbility, effect, strategy, towerData.fireRate);
        }

        public void ApplyHoverColor()
        {
            foreach (Renderer renderer in renderers)
            {
                if (renderer.material.HasProperty("_Color"))
                {
                    renderer.material.color = hoverColor;
                }
            }
        }

        public void ResetColor()
        {
            foreach (Renderer renderer in renderers)
            {
                if (renderer.material.HasProperty("_Color"))
                {
                    renderer.material.color = defaultColor;
                }
            }
        }
        public void SetStrategy(IStrategy _strategy)
        {
            this.strategy = _strategy;
            m_shooter.Initialize(towerData.range, towerData.dps, specialAbility, effectType, _strategy, towerData.fireRate);
        }

        private void TestDecorators()
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

        public void ChangeMaterial(string effectType)
        {
            if (towerData == null || towerData.materials == null)
                return;

            renderers = GetComponentsInChildren<Renderer>();
            Material materialToApply = towerData.materials.GetMaterial(effectType);

            if (materialToApply != null)
            {
                foreach (Renderer r in renderers)
                {
                    r.material = materialToApply;
                    if (r.material.HasProperty("_Color"))
                        defaultColor = r.material.color;
                }
            }
        }
    }
}