using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

public abstract class TowerFactory : MonoBehaviour
{
    public abstract Tower CreateTower(Vector3 position);
    public abstract Tower UpgradeTower(Vector3 position);
    public abstract void Notify();
}
