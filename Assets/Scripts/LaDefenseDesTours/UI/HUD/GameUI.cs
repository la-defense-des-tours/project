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

    /// <summary>
    /// An object that manages user interaction with the game. Its responsibilities deal with
    /// <list type="bullet">
    ///     <item>
    ///         <description>Building towers</description>
    ///     </item>
    ///     <item>
    ///         <description>Selecting towers and units</description>
    ///     </item>
    /// </list>
    /// </summary>
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

        /// <summary>
        /// Gets the current UI state
        /// </summary>
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

        /// <summary>
        /// The UI controller for displaying individual tower data
        /// </summary>
        public TowerUI towerUI;

        /// <summary>
        /// The UI controller for displaying tower information
        /// whilst placing
        /// </summary>
        public BuildInfoUI buildInfoUI;

        /// <summary>
        /// Fires when the <see cref="State"/> changes
        /// should only allow firing when TouchUI is used
        /// </summary>
        public event Action<State, State> stateChanged;


        /// <summary>
        /// Fires when a tower is selected/deselected
        /// </summary>
        public event Action<TowerData> selectionChanged;

        /// <summary>
        /// Our cached camera reference
        /// </summary>
        Camera m_Camera;


        /// <summary>
        /// Gets the current selected tower
        /// </summary>
        public TowerData currentSelectedTower { get; private set; }

        private float gameTimer = 0f;
        private bool isTimerRunning = false;

        [Header("Timer UI")]
        [SerializeField] private Text timerText;

        /// <summary>
        /// Gets whether a tower has been selected
        /// </summary>
        public bool isTowerSelected
        {
            get { return currentSelectedTower != null; }
        }

        /// <summary>
        /// Gets whether certain build operations are valid
        /// </summary>
        public bool isBuilding
        {
            get
            {
                return state == State.Building || state == State.BuildingWithDrag;
            }
        }

        /// <summary>
        /// Cancel placing the ghost
        /// </summary>
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

            Debug.Log("Returning to Normal state");
            SetState(State.Normal);
        }




        /// <summary>
        /// Changes the state and fires <see cref="stateChanged"/>
        /// </summary>
        /// <param name="newState">The state to change to</param>
        /// <exception cref="ArgumentOutOfRangeException">thrown on an invalid state</exception>
        public void SetState(State newState)
        {
            Debug.Log(newState.ToString());
            if (state == newState)
            {
                return;
            }
            State oldState = state;

            if (oldState == State.Paused || oldState == State.GameOver)
            {
                Time.timeScale = 1f;
                CancelGhostPlacement();
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

        /// <summary>
        /// Called when the game is over
        /// </summary>
        public void GameOver()
        {
            SetState(State.GameOver);
        }

        /// <summary>
        /// Pause the game and display the pause menu
        /// </summary>
        public void Pause()
        {
            SetState(State.Paused);
        }

        /// <summary>
        /// Resume the game and close the pause menu
        /// </summary>
        public void Unpause()
        {
            SetState(State.Normal);
        }

        /// <summary>
        /// Sets the UI into a build state for a given tower
        /// </summary>
        /// <param name="towerToBuild">
        /// The tower to build
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Throws exception trying to enter Build Mode when not in Normal Mode
        /// </exception>
        public void SetToBuildMode([NotNull] TowerData towerToBuild)
        {
            if (state == State.Paused || state == State.GameOver)
            {
                Debug.LogWarning("Cannot enter build mode while game is paused or over.");
                return;
            }

            if (state != State.Normal && state != State.BuildingWithDrag)
            {
                Debug.Log($"Forcing state reset from {state} to Normal before entering Building");
                SetState(State.Normal);
            }

            SetUpGhostTower(towerToBuild);
            SetState(State.Building);
        }


        /// <summary>
        /// Sets up the radius visualizer for a tower or ghost tower
        /// </summary>
        public void SetupRadiusVisualizer(TowerData tower, Transform ghost = null)
        {
            radiusVisualizerController.SetupRadiusVisualizers(tower, ghost);
        }


        /// <summary>
        /// Activates the tower controller UI with the specific information
        /// </summary>
        /// <param name="tower">
        /// The tower controller information to use
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Throws exception when selecting tower when <see cref="State" /> does not equal <see cref="State.Normal" />
        /// </exception>
        public void SelectTower(TowerData tower)
        {
            if (state != State.Normal)
            {
                throw new InvalidOperationException("Trying to select whilst not in a normal state");
            }
            //DeselectTower();
            currentSelectedTower = tower;
            if (currentSelectedTower != null)
            {
                //currentSelectedTower.removed += OnTowerDied;
            }
            radiusVisualizerController.SetupRadiusVisualizers(tower);

            selectionChanged?.Invoke(tower);
        }

        /// <summary>
        /// Set initial values, cache attached components
        /// and configure the controls
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            SetState(State.Normal);

            m_Camera = GetComponent<Camera>();

            isTimerRunning = false;
            gameTimer = 0f;
            UpdateTimerUI();
        }

        private void Update()
        {
            if (isTimerRunning)
            {
                gameTimer += Time.deltaTime;
                UpdateTimerUI();
            }
        }

        /// <summary>
        /// Formats the game timer into a string (MM:SS)
        /// </summary>
        private string FormatTimer()
        {
            int minutes = Mathf.FloorToInt(gameTimer / 60f);
            int seconds = Mathf.FloorToInt(gameTimer % 60f);
            return $"{minutes:00}:{seconds:00}";
        }

        /// <summary>
        /// Updates the UI with the current timer value
        /// </summary>
        private void UpdateTimerUI()
        {
            string timerTextString = FormatTimer();
            if (timerText != null)
            {
                timerText.text = timerTextString;
            }
        }

        /// <summary>
        /// Resets the game timer to zero
        /// </summary>
        public void StartTimer()
        {
            gameTimer = 0f;
            isTimerRunning = true;
            UpdateTimerUI();
        }

        /// <summary>
        /// Reset TimeScale if game is paused
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();
            Time.timeScale = 1f;
        }

        /// <summary>
        /// Subscribe to the level manager
        /// </summary>
        protected virtual void OnEnable()
        {
            if (LevelManager.instanceExists)
            {
                //LevelManager.instance.currency.currencyChanged += OnCurrencyChanged;
            }
        }

        /// <summary>
        /// Unsubscribe from the level manager
        /// </summary>
        protected virtual void OnDisable()
        {
            if (LevelManager.instanceExists)
            {
                //LevelManager.instance.currency.currencyChanged -= OnCurrencyChanged;
            }
        }

        /// <summary>
        /// Creates a new UIPointer holding data object for the given pointer position
        /// </summary>
        protected UIPointer WrapPointer(PointerInfo pointerInfo)
        {
            return new UIPointer
            {
                overUI = IsOverUI(pointerInfo),
                pointer = pointerInfo,
                ray = m_Camera.ScreenPointToRay(pointerInfo.currentPosition)
            };
        }

        /// <summary>
        /// Checks whether a given pointer is over any UI
        /// </summary>
        /// <param name="pointerInfo">The pointer to test</param>
        /// <returns>True if the event system reports this pointer being over UI</returns>
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


        ///// <summary>
        ///// Creates and hides the tower and shows the buildInfoUI
        ///// </summary>
        ///// <exception cref="ArgumentNullException">
        ///// Throws exception if the <paramref name="towerToBuild"/> is null
        ///// </exception>
        void SetUpGhostTower([NotNull] TowerData towerToBuild)
        {
            if (towerToBuild == null)
            {
                throw new ArgumentNullException("towerToBuild");
            }

            //m_CurrentTower = Instantiate(towerToBuild.towerGhostPrefab);
            //m_CurrentTower.Initialize(towerToBuild);
            //m_CurrentTower.Hide();

            ////activate build info
            if (buildInfoUI != null)
            {
                buildInfoUI.Show(towerToBuild);
            }
        }

    }
}