using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject menu;
    private Cell target;

    public void SetTarget(Cell target)
    {
        this.target = target;

        if (target.tower.currentLevel >= 3)
        {
            Debug.Log("Tower is already at maximum upgrade level. Upgrade menu will not be shown.");
            return;
        }

        transform.position = target.GetBuildPosition();

        menu.SetActive(true);
    }

    public void Hide()
    {
        menu.SetActive(false);
    }

    public void Upgrade()
    {
        Debug.Log("Upgrade button pressed");

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

        TowerManager.instance.UpgradeTower(target);
        Hide();
    }
}
