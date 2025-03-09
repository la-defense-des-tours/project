using System;
using Assets.Scripts.Core.Utilities;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;
using Assets.Scripts.LaDefenseDesTours.Towers.Data;
using Core.Input;
using JetBrains.Annotations;
using TowerDefense.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.LaDefenseDesTours.UI.HUD
{
    /// <summary>
    /// A game UI wrapper for a pointer that also contains raycast information
    /// </summary>
    public struct UIPointer
    {
        /// <summary>
        /// The pointer info
        /// </summary>
        public PointerInfo pointer;

        /// <summary>
        /// The ray for this pointer
        /// </summary>
        public Ray ray;

        /// <summary>
        /// The raycast hit object into the 3D scene
        /// </summary>
        public RaycastHit? raycast;

        /// <summary>
        /// True if this pointer started over a UI element or anything the event system catches
        /// </summary>
        public bool overUI;
    }

    [RequireComponent(typeof(Camera))]
    public class GameUI : Singleton<GameUI>
    {
        /// <summary>
        /// The states the UI can be in
        /// </summary>
        public enum State
        {
            /// <summary>
            /// The game is in its normal state. Here the player can pan the camera, select units and towers
            /// </summary>
            Normal,

            /// <summary>
            /// The game is in 'build mode'. Here the player can pan the camera, confirm or deny placement
            /// </summary>
            Building,

            /// <summary>
            /// The game is Paused. Here, the player can restart the level, or quit to the main menu
            /// </summary>
            Paused,

            /// <summary>
            /// The game is over and the level was failed/completed
            /// </summary>
            GameOver,

            /// <summary>
            /// The game is in 'build mode' and the player is dragging the ghost tower
            /// </summary>
            BuildingWithDrag
        }

        public State state { get; private set; }

        /// <summary>
        /// The radius of the sphere cast
        /// for checking ghost placement
        /// </summary>
        public float sphereCastRadius = 1;

        /// <summary>
        /// Component that manages the radius visualizers of ghosts and towers
        /// </summary>
        public RadiusVisualizerController radiusVisualizerController;
        public TowerUI towerUI;
        public BuildInfoUI buildInfoUI;
        public event Action<State, State> stateChanged;
        public event Action<Tower> selectionChanged;
        Camera m_Camera;
        public Tower currentSelectedTower { get; private set; }
        public float gameTimer = 0f;
        private bool isTimerRunning = false;
        [Header("Timer UI")]
        [SerializeField] private Text timerText;

        public bool isTowerSelected
        {
            get { return currentSelectedTower != null; }
        }

        public bool isBuilding
        {
            get
            {
                return state == State.Building || state == State.BuildingWithDrag;
            }
        }
        public void CancelGhostPlacement()
        {
            if (!isBuilding)
            {
                Debug.LogWarning("Tried to cancel ghost placement but was not in building mode.");
                return;
            }

            if (buildInfoUI != null)
            {
                buildInfoUI.Hide();
            }

            SetState(State.Normal);
        }

        public void SetState(State newState)
        {
            if (state == newState)
            {
                return;
            }
            State oldState = state;

            if (oldState == State.Paused || oldState == State.GameOver)
            {
                Time.timeScale = 1f;
            }

            switch (newState)
            {
                case State.Normal:
                    break;
                case State.Building:
                    break;
                case State.BuildingWithDrag:
                    break;
                case State.Paused:
                case State.GameOver:
                    if (oldState == State.Building)
                    {
                        CancelGhostPlacement();
                    }
                    Time.timeScale = 0f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("newState", newState, null);
            }
            state = newState;
            if (stateChanged != null)
            {
                Debug.Log($"State changed from {oldState} to {state}");
                stateChanged(oldState, state);
            }
        }

        public void GameOver()
        {
            SetState(State.GameOver);
        }

        public void Pause()
        {
            SetState(State.Paused);
        }
        public void Unpause()
        {
            SetState(State.Normal);
        }

        public void SetToBuildMode([NotNull] Tower towerToBuild)
        {
            if (state == State.Paused || state == State.GameOver)
            {
                Debug.LogWarning("Cannot enter build mode while game is paused or over.");
                return;
            }

            if (state != State.Normal && state != State.BuildingWithDrag)
            {
                SetState(State.Normal);
            }

            SetUpGhostTower(towerToBuild);
            SetState(State.Building);
        }

        public void SetupRadiusVisualizer(TowerData tower, Transform ghost = null)
        {
            radiusVisualizerController.SetupRadiusVisualizers(tower, ghost);
        }

        public void SelectTower(Tower tower)
        {
            if (state != State.Normal)
            {
                throw new InvalidOperationException("Trying to select whilst not in a normal state");
            }
            currentSelectedTower = tower;
            if (currentSelectedTower != null)
            {
                //currentSelectedTower.removed += OnTowerDied;
            }
            //radiusVisualizerController.SetupRadiusVisualizers(tower);

            selectionChanged?.Invoke(tower);
        }

        protected override void Awake()
        {
            base.Awake();

            SetState(State.Normal);

            m_Camera = GetComponent<Camera>();

            isTimerRunning = false;
            gameTimer = 0f;
            UpdateTimerUI();
            stateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(State oldState, State newState)
        {
            if (LevelManager.instance == null) return;

            AudioSource audioSource = LevelManager.instance.GetComponent<AudioSource>();
            if (audioSource == null) return;

            if (newState == State.Paused || newState == State.GameOver)
            {
                audioSource.Pause();
            }
            else if (oldState == State.Paused && newState == State.Normal)
            {
                audioSource.UnPause();
            }
        }

        private void Update()
        {
            if (isTimerRunning)
            {
                gameTimer += Time.deltaTime;
                UpdateTimerUI();
            }
        }

        private string FormatTimer()
        {
            int minutes = Mathf.FloorToInt(gameTimer / 60f);
            int seconds = Mathf.FloorToInt(gameTimer % 60f);
            return $"{minutes:00}:{seconds:00}";
        }

        private void UpdateTimerUI()
        {
            string timerTextString = FormatTimer();
            if (timerText != null)
            {
                timerText.text = timerTextString;
            }
        }

        public void StartTimer()
        {
            gameTimer = 0f;
            isTimerRunning = true;
            UpdateTimerUI();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Time.timeScale = 1f;
        }
        protected virtual void OnEnable()
        {
            if (LevelManager.instanceExists)
            {
                // LevelManager.instance.currency.currencyChanged += OnCurrencyChanged;
            }
        }

        protected virtual void OnDisable()
        {
            if (LevelManager.instanceExists)
            {
                // LevelManager.instance.currency.currencyChanged -= OnCurrencyChanged;
            }
        }
        protected UIPointer WrapPointer(PointerInfo pointerInfo)
        {
            return new UIPointer
            {
                overUI = IsOverUI(pointerInfo),
                pointer = pointerInfo,
                ray = m_Camera.ScreenPointToRay(pointerInfo.currentPosition)
            };
        }

        protected bool IsOverUI(PointerInfo pointerInfo)
        {
            int pointerId;
            EventSystem currentEventSystem = EventSystem.current;

            // Pointer id is negative for mouse, positive for touch
            var cursorInfo = pointerInfo as MouseCursorInfo;
            var mbInfo = pointerInfo as MouseButtonInfo;
            var touchInfo = pointerInfo as TouchInfo;

            if (cursorInfo != null)
            {
                pointerId = PointerInputModule.kMouseLeftId;
            }
            else if (mbInfo != null)
            {
                // LMB is 0, but kMouseLeftID = -1;
                pointerId = -mbInfo.mouseButtonId - 1;
            }
            else if (touchInfo != null)
            {
                pointerId = touchInfo.touchId;
            }
            else
            {
                throw new ArgumentException("Passed pointerInfo is not a TouchInfo or MouseCursorInfo", "pointerInfo");
            }

            return currentEventSystem.IsPointerOverGameObject(pointerId);
        }

        void SetUpGhostTower([NotNull] Tower towerToBuild)
        {
            if (towerToBuild == null)
            {
                throw new ArgumentNullException("towerToBuild");
            }
            if (buildInfoUI != null)
            {
                buildInfoUI.Show(towerToBuild);
            }
        }

    }
}