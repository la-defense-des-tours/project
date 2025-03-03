//using TowerDefense.Level;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI.HUD
{
	[RequireComponent(typeof(Canvas))]
	public class WaveUI : MonoBehaviour
	{
		public Text display;

		public Image waveFillImage;

		protected Canvas m_Canvas;

        protected virtual void Start()
		{
			m_Canvas = GetComponent<Canvas>();
			m_Canvas.enabled = false;
			LevelManager.instance.OnLevelChanged += UpdateDisplay;
        }

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
        protected void OnDestroy()
		{
			LevelManager.instance.OnLevelChanged -= UpdateDisplay;
        }
	}
}