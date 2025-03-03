using Assets.Scripts.Core.UI;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.UI.HUD
{
	public class TowerDefenseMainMenu : MainMenu
	{

		public OptionsMenu optionsMenu;
		public SimpleMainMenuPage titleMenu;
		public LeaderboardMenuPage leaderboardMenu;


		public void ShowOptionsMenu()
		{
			ChangePage(optionsMenu);
		}


		public void ShowLevelboardMenu()
		{
			ChangePage(leaderboardMenu);
		}

		/// <summary>
		/// Returns to the title screen
		/// </summary>
		public void ShowTitleScreen()
		{
			Back(titleMenu);
		}

		/// <summary>
		/// Set initial page
		/// </summary>
		protected virtual void Awake()
		{
			ShowTitleScreen();
		}

		/// <summary>
		/// Escape key input
		/// </summary>
		protected virtual void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
			{
				if ((SimpleMainMenuPage)m_CurrentPage == titleMenu)
				{
					Application.Quit();
				}
				else
				{
					Back();
				}
			}
		}
	}
}