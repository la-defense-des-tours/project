using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Level;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.UI.HUD
{
    public class BuildSidebar : MonoBehaviour
    {
        public TowerSpawnButton towerSpawnButton;

        protected virtual void Start()
        {
            if (!LevelManager.instanceExists || LevelManager.instance == null)
            {
                Debug.LogError("[UI] No level manager for tower list");
            }
            else if (LevelManager.instance.towerLibrary == null)
            {
                Debug.LogError("[UI] Tower library is null in LevelManager!");
            }
            else if (LevelManager.instance.towerLibrary.configurations == null || LevelManager.instance.towerLibrary.configurations.Count == 0)
            {
                Debug.LogError("[UI] Tower library is empty or configurations list is null!");
            }
            else
            {
                foreach (Tower tower in LevelManager.instance.towerLibrary)
                {
                    if (tower == null)
                    {
                        continue;
                    }

                    TowerSpawnButton button = Instantiate(towerSpawnButton, transform);
                    button.InitializeButton(tower);
                    button.buttonTapped += OnButtonTapped;
                }
            }
        }

        void OnButtonTapped(Tower tower)
        {
            var gameUI = GameUI.instance;
            if (gameUI.isBuilding)
            {
                gameUI.CancelGhostPlacement();

            }
            gameUI.SetToBuildMode(tower);
        }


        void OnDestroy()
        {
            TowerSpawnButton[] childButtons = GetComponentsInChildren<TowerSpawnButton>();

            foreach (TowerSpawnButton towerButton in childButtons)
            {
                towerButton.buttonTapped -= OnButtonTapped;
            }
        }
        public void StartWaveButtonPressed()
        {
            if (LevelManager.instanceExists)
            {
                LevelManager.instance.BuildingCompleted();
            }
        }
    }
}
