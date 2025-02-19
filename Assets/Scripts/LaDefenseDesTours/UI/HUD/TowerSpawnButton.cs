using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.LaDefenseDesTours.Towers.Data;

namespace Assets.Scripts.LaDefenseDesTours.UI.HUD
{
    /// <summary>
    /// A button controller for spawning towers
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class TowerSpawnButton : MonoBehaviour, IDragHandler
    {
        /// <summary>
        /// The text attached to the button
        /// </summary>
        public Text buttonText;

        public Image towerIcon;

        public Button buyButton;

        public Image energyIcon;

        public Color energyDefaultColor;

        public Color energyInvalidColor;

        /// <summary>
        /// Fires when the button is tapped
        /// </summary>
        public event Action<TowerData> buttonTapped;

        /// <summary>
        /// Fires when the pointer is outside of the button bounds
        /// and still down
        /// </summary>
        //public event Action<TowerData> draggedOff;

        /// <summary>
        /// The tower controller that defines the button
        /// </summary>
        TowerData m_Tower;

        /// <summary>
        /// Cached reference to level currency
        /// </summary>
        //Currency m_Currency;

        /// <summary>
        /// The attached rect transform
        /// </summary>
        RectTransform m_RectTransform;


        /// <summary>
        /// Checks if the pointer is out of bounds
        /// and then fires the draggedOff event
        /// </summary>
        public virtual void OnDrag(PointerEventData eventData)
        {
        //    if (!RectTransformUtility.RectangleContainsScreenPoint(m_RectTransform, eventData.position))
        //    {
        //        if (draggedOff != null)
        //        {
        //            draggedOff(m_Tower);
        //        }
        //    }
        }

        protected virtual void Awake()
        {
            Debug.Log($"TowerSpawnButton {gameObject.name} initialized");

            if (TowerManager.Instance != null)
            {
                TowerManager.Instance.RegisterSpawnButton(this); 
            }
            else
            {
                Debug.LogError("TowerManager Instance is null, TowerSpawnButton cannot register!");
            }

            m_RectTransform = (RectTransform)transform;
        }


        /// <summary>
        /// Define the button information for the tower
        /// </summary>
        /// <param name="towerData">
        /// The tower to initialize the button with
        /// </param>
        public void InitializeButton(TowerData towerData)
        {
            m_Tower = towerData;

            buttonText.text = m_Tower.cost.ToString();
            towerIcon.sprite = m_Tower.icon;


            if (LevelManager.instanceExists)
            {
                //m_Currency = LevelManager.instance.currency;
                //m_Currency.currencyChanged += UpdateButton;
            }
            else
            {
                Debug.LogWarning("[Tower Spawn Button] No level manager to get currency object");
            }
            UpdateButton();
        }


        /// <summary>
        /// Unsubscribe from events
        /// </summary>
        protected virtual void OnDestroy()
        {
            //if (m_Currency != null)
            //{
            //    m_Currency.currencyChanged -= UpdateButton;
            //}
        }

        /// <summary>
        /// The click for when the button is tapped
        /// </summary>
        public void OnClick()
        {  
            Debug.Log("m_Tower: " + m_Tower);

            buttonTapped(m_Tower);


        }

        /// <summary>
        /// Update the button's button state based on cost
        /// </summary>
        void UpdateButton()
        {
            //if (m_Currency == null)
            //{
            //    return;
            //}

            //// Enable button
            //if (m_Currency.CanAfford(m_Tower.purchaseCost) && !buyButton.interactable)
            //{
            //    buyButton.interactable = true;
            //    energyIcon.color = energyDefaultColor;
            //}
            //else if (!m_Currency.CanAfford(m_Tower.purchaseCost) && buyButton.interactable)
            //{
            //    buyButton.interactable = false;
            //    energyIcon.color = energyInvalidColor;
            //}
        }
    }
}
