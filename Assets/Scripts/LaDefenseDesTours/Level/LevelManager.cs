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
		public Player playerHomeBase
		{
			get { return homeBase; }
		}
		public bool isGameOver
		{
			get { return (levelState == LevelState.Lose); }
		}
		public event Action levelFailed;
		public event Action<LevelState, LevelState> levelStateChanged;
		public event Action homeBaseDestroyed;
        public event Action OnLevelChanged;
        public int currentLevel = 1;
        private int remainingEnemiesByLevel;
        public Leaderboard leaderboard;
        private AudioSource audioSource;
        public AudioClip normalMusic; 
        public AudioClip bossMusic;
        public AudioClip victoryMusic;
        private Coroutine fadeCoroutine;


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

            leaderboard = new Leaderboard();
            audioSource = GetComponent<AudioSource>();

            waveManager.OnBossWaveStarted += OnBossSpawned;


        }

        public Vector3 GetEnemyEndPoint()
        {
            return homeBase.transform.position;
        }

        public Vector3 GetEnemyStartPoint()
        {
            return waveManager.GetSpawnPoint();
        }
        private IEnumerator FadeMusic(AudioClip newClip)
        {
            if (audioSource == null || newClip == null) yield break;

            float startVolume = audioSource.volume;

            audioSource.clip = newClip;
            audioSource.Play();

            audioSource.volume = startVolume;
        }




        public void BuildingCompleted()
        {
            Debug.Log("[LevelManager] Construction terminée, passage à SpawningEnemies...");
            ChangeLevelState(LevelState.SpawningEnemies);
        }

        public float GetRatio()
        {
            return (float)remainingEnemiesByLevel / GetTotalEnemies();
        }
		protected override void OnDestroy()
		{
			base.OnDestroy();

            if (intro != null)
            {
                intro.introCompleted -= IntroCompleted;
            }

            if (playerHomeBase != null)
            {
                playerHomeBase.OnPlayerDeath -= OnHomeBaseDestroyed;
            }

            EnemyDeathEvent.OnEnemyDeath -= HandleEnemyDeath;
            waveManager.OnBossWaveStarted -= OnBossSpawned;
        }

        private void IntroCompleted()
        {
            ChangeLevelState(LevelState.Building);
        }




		private void ChangeLevelState(LevelState newState)
		{
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

		private void OnHomeBaseDestroyed()
		{

            string playerName = Player.GetInstance().Name;
            int levelReached = currentLevel;
            float timeSurvived = Time.timeSinceLevelLoad;

            leaderboard.AddEntry(playerName, levelReached, timeSurvived);
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
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }
                fadeCoroutine = StartCoroutine(PlayVictoryThenNormalMusic());


                StartCoroutine(WaitAndStartWave());
                NextLevel();

                remainingEnemiesByLevel = GetTotalEnemies();

                

                Debug.Log($"Niveau {currentLevel} atteint ! Nouveaux ennemis : {remainingEnemiesByLevel}");
            }
        }

        private IEnumerator PlayVictoryThenNormalMusic()
        {
            yield return StartCoroutine(FadeMusic(victoryMusic));
            yield return new WaitForSeconds(victoryMusic.length); 
            yield return StartCoroutine(FadeMusic(normalMusic));
        }

        private void OnBossSpawned()
        {
            Debug.Log("Boss wave started !");
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeMusic(bossMusic));
        }

        private IEnumerator WaitAndStartWave()
        {
            Debug.Log("Attente avant la prochaine vague...");
            yield return new WaitForSeconds(5f);
            waveManager.StartWave();
            Debug.Log($"Niveau {currentLevel} atteint ! Nouveaux ennemis : {remainingEnemiesByLevel}");
        }

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