using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Towers.Data
{
    [System.Serializable]
    public class MaterialEntry
    {
        public string effectType;
        public Material material;
    }

    [CreateAssetMenu(fileName = "TowerMaterial.asset", menuName = "La défense des tours/Tower Material", order = 1)]
    public class TowerMaterial : ScriptableObject
    {
        public Material defaultMaterial;
        public List<MaterialEntry> materials = new();

        public Material GetMaterial(string effectType)
        {
            foreach (MaterialEntry entry in materials)
            {
                if (entry.effectType == effectType)
                    return entry.material;
            }

            return defaultMaterial;
        }
    }
}