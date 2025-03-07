using System;
using System.Collections;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Utilities;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Towers.Data;
using TowerDefense.Level;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Level
{
    [RequireComponent(typeof(WaveManager))]
    public class LevelManager : Singleton<LevelManager>
    {
        public static new LevelManager instance;
        public LevelIntro intro;
        public TowerLibrary towerLibrary;
        public int startingCurrency;
        public Player homeBase;
        public WaveManager waveManager { get; protected set; }
        public LevelState levelState { get; protected set; }
        public Currency currency { get; protected set; }
        public Player playerHomeBase => homeBase;
        public bool isGameOver => levelState == LevelState.Lose;

        public event Action levelFailed;
        public event Action<LevelState, LevelState> levelStateChanged;
        public event Action homeBaseDestroyed;
        public event Action OnLevelChanged;

        public int currentLevel = 1;
        private int remainingEnemiesByLevel;
        public Leaderboard leaderboard;
        private LevelSoundManager soundManager;

        protected override void Awake()
        {
            instance = this;
            base.Awake();
            waveManager = GetComponent<WaveManager>();
            soundManager = GetComponent<LevelSoundManager>();
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

            leaderboard = new Leaderboard();
            waveManager.OnBossWaveStarted += OnBossSpawned;
            soundManager.PlayNormalMusic();
        }

        public Vector3 GetEnemyEndPoint() => homeBase.transform.position;
        public Vector3 GetEnemyStartPoint() => waveManager.GetSpawnPoint();

        public void BuildingCompleted()
        {
            Debug.Log("[LevelManager] Construction terminée, passage à SpawningEnemies...");
            ChangeLevelState(LevelState.SpawningEnemies);
        }

        public float GetRatio() => (float)remainingEnemiesByLevel / GetTotalEnemies();

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (intro != null) intro.introCompleted -= IntroCompleted;
            if (playerHomeBase != null) playerHomeBase.OnPlayerDeath -= OnHomeBaseDestroyed;
            EnemyDeathEvent.OnEnemyDeath -= HandleEnemyDeath;
            waveManager.OnBossWaveStarted -= OnBossSpawned;
        }

        private void IntroCompleted() => ChangeLevelState(LevelState.Building);

        private void ChangeLevelState(LevelState newState)
        {
            if (levelState == newState) return;
            LevelState oldState = levelState;
            levelState = newState;
            levelStateChanged?.Invoke(oldState, newState);

            if (newState == LevelState.SpawningEnemies)
            {
                OnLevelChanged?.Invoke();
                waveManager.StartWave();
            }
            else if (newState == LevelState.Lose)
            {
                SafelyCallLevelFailed();
            }
        }

        private void OnHomeBaseDestroyed()
        {
            leaderboard.AddEntry(Player.GetInstance().Name, currentLevel, Time.timeSinceLevelLoad);
            homeBaseDestroyed?.Invoke();
            if (!isGameOver)
            {
                ChangeLevelState(LevelState.Lose);
            }
        }

        private void HandleEnemyDeath(int rewardAmount)
        {
            currency.AddCurrency(rewardAmount);
            Debug.Log($"Gagné {rewardAmount} currency ! Total: {currency.currentCurrency}");

            remainingEnemiesByLevel--;

            if (remainingEnemiesByLevel <= 0)
            {
                StartCoroutine(soundManager.PlayVictoryThenNormalMusic());
                StartCoroutine(WaitAndStartWave());
                NextLevel();
                if (currentLevel % 5 == 0) 
                    Player.GetInstance().UpgradeLife();
                remainingEnemiesByLevel = GetTotalEnemies();
                Debug.Log($"Niveau {currentLevel} atteint ! Nouveaux ennemis : {remainingEnemiesByLevel}");
            }
        }

        private void OnBossSpawned()
        {
            Debug.Log("Boss wave started !");
            soundManager.PlayBossMusic();
        }

        private IEnumerator WaitAndStartWave()
        {
            Debug.Log("Attente avant la prochaine vague...");
            yield return new WaitForSeconds(5f);
            waveManager.StartWave();
            Debug.Log($"Niveau {currentLevel} atteint ! Nouveaux ennemis : {remainingEnemiesByLevel}");
        }

        private void SafelyCallLevelFailed() => levelFailed?.Invoke();
        public int GetLevel() => currentLevel;
        private void NextLevel()
        {
            currentLevel++;
            OnLevelChanged?.Invoke();
        }
        private int GetTotalEnemies() => 20 * currentLevel + 1;
    }
}