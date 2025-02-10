using Assets.Scripts.LaDefenseDesTours.Game;
using UnityEngine;
using UnityEngine.UI;
using GameUIState = Assets.Scripts.LaDefenseDesTours.UI.HUD.GameUI.State;
using Assets.Scripts.LaDefenseDesTours.Level;
using Assets.Scripts.LaDefenseDesTours.UI.HUD;
using System;

namespace Assets.Scripts.LaDefenseDesTours.UI
{
    /// <summary>
    /// In-game pause menu
    /// </summary>
    public class PauseMenu : MonoBehaviour
	{
		/// <summary>
		/// Enum to represent state of pause menu
		/// </summary>
		protected enum State
		{
			Open,
			LevelSelectPressed,
			RestartPressed,
			Closed
		}

		/// <summary>
		/// The CanvasGroup that holds the pause menu UI
		/// </summary>
		public Canvas pauseMenuCanvas;

		public Text titleText;
		
		/// <summary>
		/// The buttons present in the pause menu
		/// </summary>
		public Button levelSelectConfirmButton;

		public Button restartConfirmButton;
		
		public Button levelSelectButton;
		
		public Button restartButton;

		public Image topPanel;

		/// <summary>
		/// Color to change the top panel to highlight confirmation button
		/// </summary>
		public Color topPanelDisabledColor = new Color(1, 1, 1, 1);

		/// <summary>
		/// State of pause menu
		/// </summary>
		protected State m_State;

		/// <summary>
		/// If the pause menu was opened/closed this frame
		/// </summary>
		bool m_MenuChangedThisFrame;

        /// <summary>
        /// Open the pause menu
        /// </summary>
        public void OpenPauseMenu()
        {
            SetPauseMenuCanvas(true);

            LevelItem level = GetAllInfo();

            if (level == null)
            {
                Debug.LogWarning("No level info found, skipping title update.");
                return;
            }

            if (titleText != null)
            {
                titleText.text = level.name;
            }

            // Mettre à jour l'état
            m_State = State.Open;
        }

        /// <summary>
        /// Récupère les informations du niveau actuel
        /// </summary>
        /// <returns>Un objet LevelItem contenant les informations du niveau</returns>
        private LevelItem GetAllInfo()
    {
        // Remplacez cela par la logique spécifique à votre jeu
        // Exemple : Si le gestionnaire de niveau est accessible globalement
        LevelManager levelManager = FindObjectOfType<LevelManager>();

        if (levelManager != null)
        {
            return levelManager.GetCurrentLevel(); // Méthode fictive pour obtenir le niveau actuel
        }

        Debug.LogWarning("LevelManager introuvable ou niveau actuel non défini.");
        return null;
    }
    

    /// <summary>
    /// Fired when GameUI's State changes
    /// </summary>
    /// <param name="oldState">The State that GameUI is leaving</param>
    /// <param name="newState">The State that GameUI is entering</param>
    protected void OnGameUIStateChanged(GameUIState oldState, GameUIState newState)
	{
		m_MenuChangedThisFrame = true;
		if (newState == GameUIState.Paused)
		{
			OpenPauseMenu();
		}
		else
		{
			ClosePauseMenu();
		}
	}

	/// <summary>
	/// Level select button pressed, display/hide confirmation button
	/// </summary>
	public void LevelSelectPressed()
	{
            // Vérifie l'état actuel et bascule correctement
            if (m_State == State.LevelSelectPressed)
            {
                // Retourner à l'état 'Open'
                m_State = State.Open;

                // Réactiver le bouton 'Restart'
                restartButton.interactable = true;

                // Restaurer la couleur du topPanel
                topPanel.color = Color.white;

                // Désactiver le bouton de confirmation pour la sélection de niveau
                levelSelectConfirmButton.gameObject.SetActive(false);

                Debug.Log("Level Select button pressed: Confirmation hidden");
            }
            else
            {
                // Passer à l'état 'LevelSelectPressed'
                m_State = State.LevelSelectPressed;

                // Désactiver le bouton 'Restart'
                restartButton.interactable = false;

                // Changer la couleur du topPanel
                topPanel.color = topPanelDisabledColor;

                // Activer le bouton de confirmation pour la sélection de niveau
                levelSelectConfirmButton.gameObject.SetActive(true);

                Debug.Log("Level Select button pressed: Confirmation shown");
            }

        }

        /// <summary>
        /// Restart button pressed, display/hide confirmation button
        /// </summary>
        /// <summary>
        /// Restart button pressed, display/hide confirmation button
        /// </summary>
        public void RestartPressed()
        {
     
             if (m_State == State.RestartPressed) 
            {
                m_State = State.Open;
                levelSelectButton.interactable = true;
                topPanel.color = Color.white;
                restartConfirmButton.gameObject.SetActive(false);

                Debug.Log("Restart button pressed: Confirmation hidden");
            }
			 else
			{
                m_State = State.RestartPressed;
                levelSelectButton.interactable = false;
                topPanel.color = topPanelDisabledColor;
                restartConfirmButton.gameObject.SetActive(true);

                Debug.Log("Restart button pressed: Confirmation shown");
            }
        }


        /// <summary>
        /// Close the pause menu
        /// </summary>
        public void ClosePauseMenu()
		{
			SetPauseMenuCanvas(false);

			levelSelectConfirmButton.gameObject.SetActive(false);
			restartConfirmButton.gameObject.SetActive(false);
			levelSelectButton.interactable = true;
			restartButton.interactable = true;
			topPanel.color = Color.white;

			m_State = State.Closed;
		}

        /// <summary>
        /// Hide the pause menu on awake
        /// </summary>
        protected void Awake()
        {
            SetPauseMenuCanvas(false);
            m_State = State.Closed; // Initialisation correcte
        }

        /// <summary>
        /// Subscribe to GameUI's stateChanged event
        /// </summary>
        protected void Start()
		{
			if (GameUI.instanceExists)
			{
				GameUI.instance.stateChanged += OnGameUIStateChanged;
			}
		}

		/// <summary>
		/// Unpause the game if the game is paused and the Escape key is pressed
		/// </summary>
		protected virtual void Update()
		{
			if (m_MenuChangedThisFrame)
			{
				m_MenuChangedThisFrame = false;
				return;
			}

			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && GameUI.instance.state == GameUIState.Paused)
			{
				Unpause();
			}
		}

		/// <summary>
		/// Show/Hide the pause menu canvas group
		/// </summary>
		protected void SetPauseMenuCanvas(bool enable)
		{
			pauseMenuCanvas.enabled = enable;
		}

		public void Pause()
		{
			if (GameUI.instanceExists)
			{
				GameUI.instance.Pause();
			}
		}

		public void Unpause()
		{
			if (GameUI.instanceExists)
			{
				GameUI.instance.Unpause();
			}
		}
	}
}