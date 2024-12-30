using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Level
{
    /// <summary>
    /// Element describing a level
    /// </summary>
    [Serializable]
    public class LevelItem
    {
        /// <summary>
        /// The id - used in persistence
        /// </summary>
        public string id;

        /// <summary>
        /// The human readable level name
        /// </summary>
        public string name;

        /// <summary>
        /// The description of the level - flavour text
        /// </summary>
        public string description;

        /// <summary>
        /// The name of the scene to load
        /// </summary>
        public string sceneName;



        /// <summary>
        /// Récupère les informations du niveau actuel
        /// </summary>
        /// <returns>Un objet LevelItem contenant les informations du niveau</returns>
        private LevelItem GetAllInfo()
        {
            // Remplacez cela par la logique spécifique à votre jeu
            // Exemple : Si le gestionnaire de niveau est accessible globalement
            LevelManager levelManager = LevelManager.instance;

            if (levelManager != null)
            {
                return levelManager.GetCurrentLevel(); // Méthode fictive pour obtenir le niveau actuel
            }

            Debug.LogWarning("LevelManager introuvable ou niveau actuel non défini.");
            return null;
        }
    }

}

