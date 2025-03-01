
//using Core.Game;
//using Core.Health;
//using TowerDefense.Game;
//using TowerDefense.Level;
using Assets.Scripts.LaDefenseDesTours.Game;
using Assets.Scripts.LaDefenseDesTours.Level;
using TowerDefense.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.LaDefenseDesTours.UI
{
    /// <summary>
    /// UI to display the game over screen
    /// </summary>
    public class EndGameScreen : MonoBehaviour
    {


        /// <summary>
        /// AudioClip to play when failed
        /// </summary>
        public AudioClip defeatSound;

        /// <summary>
        /// AudioSource that plays the sound
        /// </summary>
        public AudioSource audioSource;

        /// <summary>
        /// The containing panel of the End Game UI
        /// </summary>
        public Canvas endGameCanvas;


        /// <summary>
        /// Name of level select screen
        /// </summary>
        public string menuSceneName = "MainMenu";



        /// <summary>
        /// Reference to the <see cref="LevelManager" />
        /// </summary>
        ///
        protected LevelManager m_LevelManager;

        [SerializeField] private Text leaderboardText;
        [SerializeField] private InputField playerNameInput;

        /// <summary>
        /// Safely unsubscribes from <see cref="LevelManager" /> events.
        /// Go back to the main menu scene
        /// </summary>
        public void GoToMainMenu()
        {
            SafelyUnsubscribe();
            SceneManager.LoadScene(menuSceneName);
        }

        /// <summary>
        /// Safely unsubscribes from <see cref="LevelManager" /> events.
        /// Reloads the active scene
        /// </summary>
        public void RestartLevel()
        {
            SafelyUnsubscribe();
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }


        /// <summary>
        /// Hide the panel if it is active at the start.
        /// Subscribe to the <see cref="LevelManager" /> completed/failed events.
        /// </summary>
        protected void Start()
        {
            LazyLoad();
            endGameCanvas.enabled = false;

            m_LevelManager.levelFailed += Defeat;
        }

        /// <summary>
        /// Shows the end game screen
        /// </summary>
        protected void OpenEndGameScreen()
        {
            endGameCanvas.enabled = true;

            if (!HUD.GameUI.instanceExists)
            {
                return;
            }
            if (HUD.GameUI.instance.state == HUD.GameUI.State.Building)
            {
                HUD.GameUI.instance.CancelGhostPlacement();
            }

            HUD.GameUI.instance.GameOver();
        }

        /// <summary>
        /// Occurs when level is failed
        /// </summary>
        protected void Defeat()
        {
            OpenEndGameScreen();

            playerNameInput.text = Player.GetInstance().Name;
            DisplayLeaderboard();
            if ((defeatSound != null) && (audioSource != null))
            {
                audioSource.PlayOneShot(defeatSound);
            }
        }

        public void OnPlayerNameSubmitted()
        {
            string playerName = playerNameInput.text;
            if (!string.IsNullOrEmpty(playerName))
            {
                Player.GetInstance().Name = playerName;
            }
        }

        private void DisplayLeaderboard()
        {
            if (leaderboardText == null)
            {
                Debug.LogError("Leaderboard Text is not assigned!");
                return;
            }

            var leaderboard = LevelManager.instance.leaderboard;
            var entries = leaderboard.GetEntries();

            string leaderboardString = "Leaderboard:\n";
            for (int i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];
                leaderboardString += $"{i + 1}. {entry.playerName} - Level {entry.levelReached} - {entry.timeSurvived:F1}s\n";
            }

            leaderboardText.text = leaderboardString;
        }

        /// <summary>
        /// Safely unsubscribes from <see cref="LevelManager" /> events.
        /// </summary>
        protected void OnDestroy()
        {
            SafelyUnsubscribe();
            if (HUD.GameUI.instanceExists)
            {
                HUD.GameUI.instance.Unpause();
            }
        }

        /// <summary>
        /// Ensure that <see cref="LevelManager" /> events are unsubscribed from when necessary
        /// </summary>
        protected void SafelyUnsubscribe()
        {
            LazyLoad();
            m_LevelManager.levelFailed -= Defeat;
        }

        /// <summary>
        /// Ensure <see cref="m_LevelManager" /> is not null
        /// </summary>
        protected void LazyLoad()
        {
            if ((m_LevelManager == null) && LevelManager.instanceExists)
            {
                m_LevelManager = LevelManager.instance;
            }
        }
    }
}