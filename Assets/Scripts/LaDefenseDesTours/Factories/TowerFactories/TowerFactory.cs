using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

public abstract class TowerFactory : MonoBehaviour
{
    public abstract Tower CreateTower(Vector3 position, int level = 1);
    public abstract void Notify();
}
