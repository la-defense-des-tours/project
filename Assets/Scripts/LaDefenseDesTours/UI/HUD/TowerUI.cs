using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;
using Assets.Scripts.LaDefenseDesTours.Towers.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.LaDefenseDesTours.UI.HUD
{
	/// <summary>
	/// Controls the UI objects that draw the tower data
	/// </summary>
	[RequireComponent(typeof(Canvas))]
	public class TowerUI : MonoBehaviour
	{
		public Text towerName;
		public Text description;
		public Text upgradeDescription;


		public Button sellButton;
		public Button upgradeButton;
		public Button fireButton;
        public Button iceButton;
        public Button lightButton;
        public TowerInfoDisplay towerInfoDisplay;

		public RectTransform panelRectTransform;

		public GameObject[] confirmationButtons;

		protected Camera m_GameCamera;

		protected Tower m_Tower;

		protected Canvas m_Canvas;

		public virtual void Show(Tower towerToShow)
		{
			if (towerToShow == null)
			{
				return;
			}
			m_Tower = towerToShow;
			AdjustPosition();

			m_Canvas.enabled = true;

			int sellValue = m_Tower.towerData.sellCost;
			if (sellButton != null)
			{
				sellButton.gameObject.SetActive(sellValue > 0);
			}
			if (upgradeButton != null)
			{
				upgradeButton.interactable =
					LevelManager.instance.currency.CanAfford(m_Tower.cost);
				bool maxLevel = m_Tower.isAtMaxLevel;
				upgradeButton.gameObject.SetActive(!maxLevel);
				if (!maxLevel)
				{
					//upgradeDescription.text =
					//	m_Tower.levels[m_Tower.currentLevel + 1].upgradeDescription.ToUpper();
				}
			}
			LevelManager.instance.currency.currencyChanged += OnCurrencyChanged;
			towerInfoDisplay.Show(towerToShow);
			foreach (var button in confirmationButtons)
			{
				button.SetActive(false);
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
			//GameUI.instance.UpgradeSelectedTower();
		}

		public void SellButtonClick()
		{
			//GameUI.instance.SellSelectedTower();
		}


        public void FireEffect()
        {
			Debug.Log("Fire effect");
            if (m_Tower != null)
            {
				m_Tower.ApplyEffect(new FireEffect(m_Tower));
				Hide();
            }
        }

		public void IceEffect()
		{
            if (m_Tower != null)
            {
                m_Tower.ApplyEffect(new IceEffect(m_Tower));
                Hide();
            }
        }

		public void LigthningEffect()
		{
            if (m_Tower != null)
            {
                m_Tower.ApplyEffect(new LightningEffect(m_Tower));
                Hide();
            }
        }
        
        protected virtual void Awake()
		{
			m_Canvas = GetComponent<Canvas>();
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
			if (m_Tower == null)
			{
				return;
			}
			Vector3 point = m_GameCamera.WorldToScreenPoint(m_Tower.transform.position);
			point.z = 0;
			panelRectTransform.transform.position = point;
		}


		protected void OnGameUIStateChanged(GameUI.State oldState, GameUI.State newState)
		{
			if (newState == GameUI.State.GameOver)
			{
				Hide();
			}
		}


		void OnCurrencyChanged()
		{
			if (m_Tower != null && upgradeButton != null)
			{
				upgradeButton.interactable =
					LevelManager.instance.currency.CanAfford(m_Tower.towerData.cost);
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
	}
}