using System.Collections.Generic;
using Assets.Scripts.LaDefenseDesTours.Interfaces;
using Assets.Scripts.LaDefenseDesTours.Towers.Data;
using Assets.Scripts.LaDefenseDesTours.UI.HUD;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private TowerFactory machineGunFactory;
    [SerializeField] private TowerFactory laserFactory;
    [SerializeField] private TowerFactory canonFactory;
    private TowerFactory selectedFactory;
    public static TowerManager Instance;
    private List<TowerSpawnButton> spawnButtons = new List<TowerSpawnButton>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple TowerManager instances detected!");
            return;
        }
        Instance = this;
    }


    public void RegisterSpawnButton(TowerSpawnButton button)
    {
        if (button == null)
        {
            Debug.LogError("Trying to register a null button in TowerManager!");
            return;
        }

        spawnButtons.Add(button);
        button.buttonTapped += OnTowerButtonTapped; // Ajoute l'événement

        Debug.Log($"Button registered: {button.name}, total buttons: {spawnButtons.Count}");
    }

    private void OnTowerButtonTapped(TowerData towerData)
    {
        SelectTower(towerData.towerName);
        Debug.Log("Tower selected");
    }

    private void SelectTower(string towerName)
    {
        switch (towerName)
        {
            case "Machine Gun":
                selectedFactory = machineGunFactory;
                break;
            case "Laser":
                selectedFactory = laserFactory;
                break;
            case "Canon":
                selectedFactory = canonFactory;
                break;
            default:
                Debug.LogError("Invalid tower name");
                break;
        }
        Debug.Log($"Selected Tower: {towerName}");
    }

    public Tower GetTowerToPlace(Vector3 position)
    {
        if (selectedFactory == null)
            return null;

        return selectedFactory.CreateTower(position);
    }
}