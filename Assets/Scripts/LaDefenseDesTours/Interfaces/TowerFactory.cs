using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

public abstract class TowerFactory : MonoBehaviour
{
    public abstract GameObject CreateTower();
    public abstract void Notify();
}
