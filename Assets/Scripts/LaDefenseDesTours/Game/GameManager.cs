using Assets.Scripts.Core;
using Assets.Scripts.LaDefenseDesTours.Level;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts.LaDefenseDesTours.Game
{
	/// <summary>
	/// Game Manager - a persistent single that handles persistence, and level lists, etc.
	/// This should be initialized when the game starts.
	/// </summary>
	public class GameManager : GameManagerBase<GameManager, GameDataStore>
	{

        /// <summary>
        /// Set sleep timeout to never sleep
        /// </summary>
        protected override void Awake()
		{
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			base.Awake();
		}



		/// <summary>
		/// Method used for completing the level
		/// </summary>
		/// <param name="levelId">The levelId to mark as complete</param>
		/// <param name="starsEarned"></param>
		public void CompleteLevel(string levelId, int starsEarned)
		{
			//if (!levelList.ContainsKey(levelId))
			//{
			//	Debug.LogWarningFormat("[GAME] Cannot complete level with id = {0}. Not in level list", levelId);
			//	return;
			//}

			m_DataStore.CompleteLevel(levelId, starsEarned);
			SaveData();
		}

	}
}
