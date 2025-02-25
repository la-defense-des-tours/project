using System;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Utilities;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Towers.Data;
using TowerDefense.Level;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Level

{
    /// <summary>
    /// The level manager - handles the level states and tracks the player's currency
    /// </summary>
    [RequireComponent(typeof(WaveManager))]
	public class LevelManager : Singleton<LevelManager>
	{
        public static new LevelManager instance;

        [SerializeField]
        private LevelItem currentLevel;
        /// <summary>
        /// The configured level intro. If this is null the LevelManager will fall through to the gameplay state (i.e. SpawningEnemies)
        /// </summary>
        public LevelIntro intro;

		///// <summary>
		///// The tower library for this level
		///// </summary>
		public TowerLibrary towerLibrary;


        /// <summary>
        /// The currency that the player starts with
        /// </summary>
        public int startingCurrency;


        ///// <summary>
        ///// The home bases that the player must defend
        ///// </summary>
        public Player homeBase;


		/// <summary>
		/// The attached wave manager
		/// </summary>
		public WaveManager waveManager { get; protected set; }


		/// <summary>
		/// The current state of the level
		/// </summary>

		public LevelState levelState { get; protected set; }

		/// <summary>
		/// The currency controller
		/// </summary>
		public Currency currency { get; protected set; }



		///// <summary>
		///// An accessor for the home bases
		///// </summary>
		public Player playerHomeBase
		{
			get { return homeBase; }
		}

		/// <summary>
		/// If the game is over
		/// </summary>
		public bool isGameOver
		{
			get { return (levelState == LevelState.Lose); }
		}


		/// <summary>
		/// Fired when all of the home bases are destroyed
		/// </summary>
		public event Action levelFailed;

		/// <summary>
		/// Fired when the level state is changed - first parameter is the old state, second parameter is the new state
		/// </summary>
		public event Action<LevelState, LevelState> levelStateChanged;


		/// <summary>
		/// Event for home base being destroyed
		/// </summary>
		public event Action homeBaseDestroyed;


		protected override void Awake()
		{
			instance = this;
			base.Awake();
			waveManager = GetComponent<WaveManager>();
			levelState = LevelState.Intro;

            currency = new Currency(startingCurrency);

            EnemyDeathEvent.OnEnemyDeath += HandleEnemyDeath;

            if (intro != null)
			{
				intro.introCompleted += IntroCompleted;
			}
			else
			{
				IntroCompleted();
			}

			if (playerHomeBase)
			{
				playerHomeBase.OnPlayerDeath += OnHomeBaseDestroyed;
			}
		}


        /// <summary>
        /// Définit le niveau actuel
        /// </summary>
        /// <param name="level">L'élément de niveau à définir</param>
        public void SetCurrentLevel(LevelItem level)
        {
            currentLevel = level;
        }

		public Vector3 GetEnemyEndPoint()
		{
			return homeBase.transform.position;
		}

        /// <summary>
        /// Récupère le niveau actuel
        /// </summary>
        /// <returns>Le niveau actuel</returns>
        public LevelItem GetCurrentLevel()
        {
            return currentLevel;
        }


		/// <summary>
		/// Completes building phase, setting state to spawn enemies
		/// </summary>
		public virtual void BuildingCompleted()
		{
			ChangeLevelState(LevelState.SpawningEnemies);
		}



		/// <summary>
		/// Updates the currency gain controller
		/// </summary>
		protected virtual void Update()
		{


		}

		/// <summary>
		/// Unsubscribes from events
		/// </summary>
		protected override void OnDestroy()
		{
			base.OnDestroy();

			if (intro != null)
			{
				intro.introCompleted -= IntroCompleted;
			}

            // Il faut détruire la base pour lancer l'événement

            if (playerHomeBase != null)
            {
                playerHomeBase.OnPlayerDeath -= OnHomeBaseDestroyed;
            }

            EnemyDeathEvent.OnEnemyDeath -= HandleEnemyDeath;
        }

        /// <summary>
        /// Fired when Intro is completed or immediately, if no intro is specified
        /// </summary>
        protected virtual void IntroCompleted()
		{
			ChangeLevelState(LevelState.Building);
		}


		/// <summary>
		/// Changes the state and broadcasts the event
		/// </summary>
		/// <param name="newState">The new state to transitioned to</param>
		protected virtual void ChangeLevelState(LevelState newState)
		{
			// If the state hasn't changed then return
			if (levelState == newState)
			{
				return;
			}

			LevelState oldState = levelState;
			levelState = newState;

            levelStateChanged?.Invoke(oldState, newState);


            switch (newState)
			{
				case LevelState.SpawningEnemies:
					waveManager.StartWave();
					break;
				case LevelState.Lose:
					SafelyCallLevelFailed();
					break;
			}
		}

		/// <summary>
		/// Fired when a home base is destroyed
		/// </summary>
		protected virtual void OnHomeBaseDestroyed()
		{

            homeBaseDestroyed?.Invoke();


            if (!isGameOver)
			{
				ChangeLevelState(LevelState.Lose);
			}
		}


        // Gère la mort d'un ennemi
        private void HandleEnemyDeath(int rewardAmount)
        {
            currency.AddCurrency(rewardAmount);
            Debug.Log($"Gagné {rewardAmount} currency ! Total: {currency.currentCurrency}");
        }

        /// <summary>
        /// Calls the <see cref="levelFailed"/> event
        /// </summary>
        protected virtual void SafelyCallLevelFailed()
		{
			levelFailed?.Invoke();
		}
	}
}