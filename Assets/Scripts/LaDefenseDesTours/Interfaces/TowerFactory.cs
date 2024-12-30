using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

public abstract class TowerFactory : MonoBehaviour
{
    public abstract Tower CreateTower();
    public abstract void Notify();
}
