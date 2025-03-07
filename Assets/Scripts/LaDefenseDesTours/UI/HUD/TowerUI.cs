using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;
using Assets.Scripts.LaDefenseDesTours.Towers.Data;
using LaDefenseDesTours.Strategy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.LaDefenseDesTours.UI.HUD
{
    [RequireComponent(typeof(Canvas))]
    public class TowerUI : MonoBehaviour
    {
        public Text towerName, upgradeDescription, firePrice, icePrice, lightPrice, UpgradePrice, SellPrice;
        public Button sellButton, upgradeButton, fireButton, iceButton, lightButton;
        public TowerInfoDisplay towerInfoDisplay;
        public RectTransform panelRectTransform;
        public GameObject[] confirmationButtons;
        public Dropdown dropdown;

        protected Camera m_GameCamera;
        protected Tower m_Tower;
        protected Canvas m_Canvas;

        public virtual void Show(Tower towerToShow)
        {
            if (towerToShow == null) return;

            m_Tower = towerToShow;
            AdjustPosition();
            m_Canvas.enabled = true;

            UpdateUIElements();

            if (dropdown != null)
            {
                dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
                SetDropdownSelection();
                
            }

            LevelManager.instance.currency.currencyChanged += OnCurrencyChanged;
            towerInfoDisplay.Show(towerToShow);
            SetConfirmationButtons(false);
        }

        private void UpdateUIElements()
        {
            int sellValue = m_Tower.towerData.sellCost;

            if (sellButton != null)
            {
                sellButton.gameObject.SetActive(sellValue > 0);
                SellPrice.text = sellValue.ToString();
            }

            if (upgradeButton != null)
            {
                upgradeButton.interactable = LevelManager.instance.currency.CanAfford(m_Tower.towerData.upgradeCost);
                bool maxLevel = m_Tower.isAtMaxLevel;
                upgradeButton.gameObject.SetActive(!maxLevel);

                if (!maxLevel)
                {
                    upgradeDescription.text = m_Tower.towerData.upgradeDescription.ToUpper();
                    UpgradePrice.text = m_Tower.towerData.upgradeCost.ToString();
                }
            }

            UpdateEffectButton(fireButton, firePrice, m_Tower.firePrice, "Fire");
            UpdateEffectButton(iceButton, icePrice, m_Tower.icePrice, "Ice");
            UpdateEffectButton(lightButton, lightPrice, m_Tower.lightPrice, "Lightning");
        }

        private void SetDropdownSelection()
        {
            if (m_Tower.GetStrategyType() != "")
            {
                string strategyName = m_Tower.GetStrategyType();
                int index = dropdown.options.FindIndex(option => option.text == strategyName);
                if (index >= 0)
                {
                    dropdown.value = index;
                }
            }
        }

        private void UpdateEffectButton(Button button, Text priceText, int price, string effectType)
        {
            if (button != null)
            {
                button.interactable = LevelManager.instance.currency.CanAfford(price) && m_Tower.effectType != effectType;
                priceText.text = price.ToString();
            }
        }

        private void SetConfirmationButtons(bool state)
        {
            foreach (var button in confirmationButtons)
            {
                button.SetActive(state);
            }
        }

        public virtual void Hide()
        {
            m_Tower = null;
            m_Canvas.enabled = false;
            LevelManager.instance.currency.currencyChanged -= OnCurrencyChanged;
        }

        public void UpgradeButtonClick()
        {
            if (LevelManager.instance.currency.TryPurchase(m_Tower.towerData.upgradeCost))
            {
                m_Tower.Upgrade();
                Hide();
            }
        }

        public void SellButtonClick()
        {
            LevelManager.instance.currency.AddCurrency(m_Tower.towerData.sellCost);
            m_Tower.Sell();
            Hide();
        }

        public void FireEffect()
        {
            if (m_Tower != null && LevelManager.instance.currency.TryPurchase(m_Tower.firePrice))
            {
                FireEffect effect = new(m_Tower);
                effect.ApplyEffect();
                Hide();
            }
        }

        public void IceEffect()
        {
            if (m_Tower != null && LevelManager.instance.currency.TryPurchase(m_Tower.icePrice))
            {
                IceEffect effect = new(m_Tower);
                effect.ApplyEffect();
                Hide();
            }
        }

        public void LightningEffect()
        {
            if (m_Tower != null && LevelManager.instance.currency.TryPurchase(m_Tower.lightPrice))
            {
                LightningEffect effect = new(m_Tower);
                effect.ApplyEffect();
                Hide();
            }
        }

        protected virtual void Awake()
        {
            m_Canvas = GetComponent<Canvas>();
        }

        protected virtual void Start()
        {
            m_GameCamera = Camera.main;
            m_Canvas.enabled = false;

            if (GameUI.instanceExists)
            {
                GameUI.instance.selectionChanged += OnUISelectionChanged;
                GameUI.instance.stateChanged += OnGameUIStateChanged;
            }
        }

        protected virtual void Update()
        {
            AdjustPosition();
        }

        protected virtual void OnDisable()
        {
            if (LevelManager.instanceExists)
            {
                LevelManager.instance.currency.currencyChanged -= OnCurrencyChanged;
            }
        }

        protected void AdjustPosition()
        {
            if (m_Tower == null) return;

            Vector3 point = m_GameCamera.WorldToScreenPoint(m_Tower.transform.position);
            point.z = 0;
            panelRectTransform.transform.position = point;
        }

        public void SetStrategy(string strategy)
        {
            if (m_Tower != null)
            {
                switch (strategy)
                {
                    case "Highest Hp":
                        m_Tower.SetStrategy(new HighestHP());
                        break;
                    case "Highest Speed":
                        m_Tower.SetStrategy(new HighestSpeed());
                        break;
                    case "Nearest Base":
                        m_Tower.SetStrategy(new NearestBase());
                        break;
                    case "Nearest Enemy":
                        m_Tower.SetStrategy(new NearestEnemy());
                        break;
                }
            }
        }

        protected void OnGameUIStateChanged(GameUI.State oldState, GameUI.State newState)
        {
            if (newState == GameUI.State.GameOver)
            {
                Hide();
            }
        }

        protected virtual void OnUISelectionChanged(Tower newTower)
        {
            if (newTower != null)
            {
                Show(newTower);
            }
            else
            {
                Hide();
            }
        }

        void OnCurrencyChanged()
        {
            if (m_Tower != null && upgradeButton != null)
            {
                upgradeButton.interactable = LevelManager.instance.currency.CanAfford(m_Tower.towerData.upgradeCost);
            }
        }

        void OnDestroy()
        {
            if (GameUI.instanceExists)
            {
                GameUI.instance.selectionChanged -= OnUISelectionChanged;
                GameUI.instance.stateChanged -= OnGameUIStateChanged;
            }
        }

        void OnDropdownValueChanged(int index)
        {
            string selectedOption = dropdown.options[index].text;
            SetStrategy(selectedOption);
        }
    }
}