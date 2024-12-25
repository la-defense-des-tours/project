using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Interfaces
{
    /// <summary>
    /// Interface for tower
    /// </summary>
    public interface Tower
    {
        /// <summary>
        /// Gets the tower's range
        /// </summary>
        float Range { get; }

        /// <summary>
        /// Gets the tower's level
        /// </summary>
        int currentLevel { get; }



        /// <summary>
        /// Gets the tower's damage
        /// </summary>
        float Damage { get; }

        /// <summary>
        /// Gets the tower's fire rate
        /// </summary>
        float FireRate { get; }

        /// <summary>
        /// Gets the tower's cost
        /// </summary>
        int Cost { get; }

        /// <summary>
        /// Gets the tower's upgrade cost
        /// </summary>
        int UpgradeCost { get; }

        /// <summary>
        /// Gets the tower's upgrade damage
        /// </summary>
        float UpgradeDamage { get; }

        /// <summary>
        /// Gets the tower's upgrade fire rate
        /// </summary>
        float UpgradeFireRate { get; }

        /// <summary>
        /// Gets the tower's upgrade range
        /// </summary>
        float UpgradeRange { get; }

        /// <summary>
        /// Gets the tower's upgrade cost
        /// </summary>
        int SellValue { get; }

        /// <summary>
        /// Gets the tower's upgrade cost
        /// </summary>
        int UpgradeSellValue { get; }

        /// <summary>
        /// Gets the tower's upgrade cost
        /// </summary>
        void Upgrade();
    }
}