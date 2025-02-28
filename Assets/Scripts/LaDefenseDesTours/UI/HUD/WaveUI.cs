//using TowerDefense.Level;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI.HUD
{
	/// <summary>
	/// A class for displaying the wave feedback
	/// </summary>
	[RequireComponent(typeof(Canvas))]
	public class WaveUI : MonoBehaviour
	{
		/// <summary>
		/// The text element to display information on
		/// </summary>
		public Text display;

		public Image waveFillImage;

		protected Canvas m_Canvas;

        /// <summary>
        /// cache the total amount of waves
        /// Update the display 
        /// and Subscribe to waveChanged
        /// </summary>
        protected virtual void Start()
		{
			m_Canvas = GetComponent<Canvas>();
			m_Canvas.enabled = false;
			LevelManager.instance.OnLevelChanged += UpdateDisplay;
        }

		/// <summary>
		/// Write the current wave amount to the display
		/// </summary>
		protected void UpdateDisplay()
		{
			m_Canvas.enabled = true;
			string output = string.Format("{0}", LevelManager.instance.currentLevel);
			display.text = output;
		}

		protected virtual void Update()
		{
			waveFillImage.fillAmount = LevelManager.instance.GetRatio();
        }

        /// <summary>
        /// Unsubscribe from events
        /// </summary>
        protected void OnDestroy()
		{
			LevelManager.instance.OnLevelChanged -= UpdateDisplay;
        }
	}
}