using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject menu;
    private Cell target;

    public void SetTarget(Cell target)
    {
        this.target = target;

        if (target.upgradeLevel >= 2)
        {
            Debug.Log("Tower is already at maximum upgrade level. Upgrade menu will not be shown.");
            return;
        }

        transform.position = target.GetBuildPosition();

        TowerManager towerManager = TowerManager.Instance;
        towerManager.GetSelectedFactory();

        menu.SetActive(true);
    }

    public void Hide()
    {
        menu.SetActive(false);
    }

    public void Upgrade()
    {
        Debug.Log("Upgrade");

        if (target == null)
        {
            Debug.LogError("No factory selected for upgrade!");
            return;
        }

        if (target.currentFactory == null)
        {
            Debug.LogError("No factory stored in this cell! Cannot upgrade.");
            return;
        }

        target.UpgradeTower();
        Hide();
    }
}
