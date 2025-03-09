using Assets.Scripts.LaDefenseDesTours.Game;
using UnityEngine;
using UnityEngine.UI;
using GameUIState = Assets.Scripts.LaDefenseDesTours.UI.HUD.GameUI.State;
using Assets.Scripts.LaDefenseDesTours.Level;
using Assets.Scripts.LaDefenseDesTours.UI.HUD;
using System;

namespace Assets.Scripts.LaDefenseDesTours.UI
{

    public class PauseMenu : MonoBehaviour
	{
		protected enum State
		{
			Open,
			LevelSelectPressed,
			RestartPressed,
			Closed
		}

		public Canvas pauseMenuCanvas;

		public Text titleText;
		
		public Button levelSelectConfirmButton;

		public Button restartConfirmButton;
		
		public Button levelSelectButton;
		
		public Button restartButton;

		public Image topPanel;

		public Color topPanelDisabledColor = new Color(1, 1, 1, 1);

		protected State m_State;

		bool m_MenuChangedThisFrame;


        public void OpenPauseMenu()
        {
            SetPauseMenuCanvas(true);

            // Mettre à jour l'état
            m_State = State.Open;
        }


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


	public void LevelSelectPressed()
	{
            if (m_State == State.LevelSelectPressed)
            {
                m_State = State.Open;
                restartButton.interactable = true;
                topPanel.color = Color.white;

                levelSelectConfirmButton.gameObject.SetActive(false);

            }
            else
            {
                m_State = State.LevelSelectPressed;
                restartButton.interactable = false;
                topPanel.color = topPanelDisabledColor;
                levelSelectConfirmButton.gameObject.SetActive(true);

            }

        }

        public void RestartPressed()
        {
     
             if (m_State == State.RestartPressed) 
            {
                m_State = State.Open;
                levelSelectButton.interactable = true;
                topPanel.color = Color.white;
                restartConfirmButton.gameObject.SetActive(false);

            }
			 else
			{
                m_State = State.RestartPressed;
                levelSelectButton.interactable = false;
                topPanel.color = topPanelDisabledColor;
                restartConfirmButton.gameObject.SetActive(true);
            }
        }

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

        protected void Awake()
        {
            SetPauseMenuCanvas(false);
            m_State = State.Closed; 
        }

        protected void Start()
		{
			if (GameUI.instanceExists)
			{
				GameUI.instance.stateChanged += OnGameUIStateChanged;
			}
		}

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