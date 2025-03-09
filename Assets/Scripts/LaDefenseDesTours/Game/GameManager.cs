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
        public string playerName;

        /// <summary>
        /// Set sleep timeout to never sleep
        /// </summary>
        protected override void Awake()
		{
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			base.Awake();
		}

        public void SetPlayerName(string name)
        {
            playerName = name;
        }

        public string GetPlayerName()
        {
            return playerName;
        }


    }
}
