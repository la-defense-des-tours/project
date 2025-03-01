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
        public new static LevelManager instance { get; private set; }

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
        private WaveManager waveManager { get; set; }


        /// <summary>
        /// The current state of the level
        /// </summary>

        private LevelState levelState { get; set; }

        /// <summary>
        /// The currency controller
        /// </summary>
        public Currency currency { get; private set; }


        ///// <summary>
        ///// An accessor for the home bases
        ///// </summary>
        private Player playerHomeBase => homeBase;

        /// <summary>
        /// If the game is over
        /// </summary>
        private bool isGameOver => (levelState == LevelState.Lose);

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

        public event Action OnLevelChanged;

        public int currentLevel = 1;

        private int remainingEnemiesByLevel;


        protected override void Awake()
        {
            instance = this;
            base.Awake();
            waveManager = GetComponent<WaveManager>();
            levelState = LevelState.Intro;

            currency = new Currency(startingCurrency);

            EnemyDeathEvent.OnEnemyDeath += HandleEnemyDeath;

            remainingEnemiesByLevel = GetTotalEnemies();

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

        public Vector3 GetEnemyEndPoint()
        {
            return homeBase.transform.position;
        }

        public Vector3 GetEnemyStartPoint()
        {
            return waveManager.GetSpawnPoint();
        }

        /// <summary>
        /// Completes building phase, setting state to spawn enemies
        /// </summary>
        public void BuildingCompleted()
        {
            Debug.Log("[LevelManager] Construction terminée, passage à SpawningEnemies...");
            ChangeLevelState(LevelState.SpawningEnemies);
        }

        public float GetRatio()
        {
            return (float)remainingEnemiesByLevel / GetTotalEnemies();
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
        private void IntroCompleted()
        {
            ChangeLevelState(LevelState.Building);
        }


        /// <summary>
        /// Changes the state and broadcasts the event
        /// </summary>
        /// <param name="newState">The new state to transitioned to</param>
        private void ChangeLevelState(LevelState newState)
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
                    OnLevelChanged?.Invoke();
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
        private void OnHomeBaseDestroyed()
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

            remainingEnemiesByLevel--;

            if (remainingEnemiesByLevel <= 0)
            {
                NextLevel();
                remainingEnemiesByLevel = GetTotalEnemies();
                waveManager.StartWave();

                Debug.Log($"Niveau {currentLevel} atteint ! Nouveaux ennemis : {remainingEnemiesByLevel}");
            }
        }

        /// <summary>
        /// Calls the <see cref="levelFailed"/> event
        /// </summary>
        private void SafelyCallLevelFailed()
        {
            levelFailed?.Invoke();
        }

        public int GetLevel()
        {
            return currentLevel;
        }

        private void NextLevel()
        {
            currentLevel++;
            OnLevelChanged?.Invoke();
        }

        private int GetTotalEnemies()
        {
            return 20 * currentLevel + 1;
        }
    }
}