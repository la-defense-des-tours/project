﻿using Assets.Scripts.LaDefenseDesTours.Interfaces;
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
    public class TowerSpawnButton : MonoBehaviour
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
        public event Action<Tower> buttonTapped;

        /// <summary>
        /// The tower controller that defines the button
        /// </summary>
        Tower m_Tower;

        /// <summary>
        /// Cached reference to level currency
        /// </summary>
        Currency m_Currency;

        /// <summary>
        /// The attached rect transform
        /// </summary>
        RectTransform m_RectTransform;



        protected virtual void Awake()
        {

            if (TowerManager.instance != null)
            {
                TowerManager.instance.RegisterSpawnButton(this); 
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
        public void InitializeButton(Tower towerData)
        {
            m_Tower = towerData;

            buttonText.text = m_Tower.towerData.cost.ToString();
            towerIcon.sprite = m_Tower.towerData.icon;


            if (LevelManager.instanceExists)
            {
                m_Currency = LevelManager.instance.currency;
                m_Currency.currencyChanged += UpdateButton;
            }
            else
            {
                Debug.LogWarning("[Tower Spawn Button] No level manager to get currency object");
            }
            UpdateButton();
        }


        /// <summary>
        /// The click for when the button is tapped
        /// </summary>
        public void OnClick()
        {  
            buttonTapped(m_Tower);

        }

        /// <summary>
        /// Update the button's button state based on cost
        /// </summary>
        void UpdateButton()
        {
            if (m_Currency == null)
            {
                return;
            }

            // Enable button
            if (m_Currency.CanAfford(m_Tower.towerData.cost) && !buyButton.interactable)
            {
                buyButton.interactable = true;
                energyIcon.color = energyDefaultColor;
            }
            else if (!m_Currency.CanAfford(m_Tower.towerData.cost) && buyButton.interactable)
            {
                buyButton.interactable = false;
                energyIcon.color = energyInvalidColor;
            }
        }


        /// <summary>
        /// Unsubscribe from events
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (m_Currency != null)
            {
                m_Currency.currencyChanged -= UpdateButton;
            }
        }

    }
}
