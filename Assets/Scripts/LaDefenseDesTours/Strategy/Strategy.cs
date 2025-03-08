using Assets.Scripts.LaDefenseDesTours.Interfaces;
using UnityEngine;

namespace LaDefenseDesTours.Strategy
{
    public interface IStrategy
    {
        Transform SelectTarget(Enemy[] enemies, Vector3 towerPosition, float range);
        string GetStrategyName();

    }
}