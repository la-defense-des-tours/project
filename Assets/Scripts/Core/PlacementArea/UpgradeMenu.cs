using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject menu;
    private Cell target;
    private readonly TowerFactory towerFactory;

    public void SetTarget(Cell target)
    {
        this.target = target;
        transform.position = target.GetBuildPosition();
        menu.SetActive(true);
    }

    public void Hide()
    {
        menu.SetActive(false);
    }

    public void Upgrade()
    {
        Debug.Log("Upgrade");
        target.UpgradeTower(towerFactory);
        Hide();
    }
}
