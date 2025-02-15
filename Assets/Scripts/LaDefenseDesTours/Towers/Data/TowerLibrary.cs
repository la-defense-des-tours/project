using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.LaDefenseDesTours.Towers.Data
{
    [CreateAssetMenu(fileName = "TowerLibrary.asset", menuName = "La défense des tours/Tower Library", order = 1)]
    public class TowerLibrary : ScriptableObject, IList<TowerData>, IDictionary<string, TowerData>
    {
        public List<TowerData> configurations;
        Dictionary<string, TowerData> m_ConfigurationDictionary;

        public TowerData this[int index]
        {
            get { return configurations[index]; }
            set { configurations[index] = value; }
        }

        public TowerData this[string key]
        {
            get { return m_ConfigurationDictionary[key]; }
            set { m_ConfigurationDictionary[key] = value; }
        }

        public ICollection<string> Keys => m_ConfigurationDictionary.Keys;
        public ICollection<TowerData> Values => m_ConfigurationDictionary.Values;

        public int Count => configurations.Count;
        public bool IsReadOnly => false;

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            if (configurations == null) return;
            m_ConfigurationDictionary = configurations.ToDictionary(t => t.towerName);
        }

        public bool ContainsKey(string key) => m_ConfigurationDictionary.ContainsKey(key);

        public void Add(string key, TowerData value) => m_ConfigurationDictionary.Add(key, value);

        public bool Remove(string key) => m_ConfigurationDictionary.Remove(key);

        public bool TryGetValue(string key, out TowerData value) => m_ConfigurationDictionary.TryGetValue(key, out value);

        public void Add(KeyValuePair<string, TowerData> item) => m_ConfigurationDictionary.Add(item.Key, item.Value);

        public bool Remove(KeyValuePair<string, TowerData> item) => m_ConfigurationDictionary.Remove(item.Key);

        public bool Contains(KeyValuePair<string, TowerData> item) => m_ConfigurationDictionary.Contains(item);

        public void CopyTo(KeyValuePair<string, TowerData>[] array, int arrayIndex)
        {
            foreach (var kvp in m_ConfigurationDictionary)
            {
                array[arrayIndex++] = kvp;
            }
        }

        public IEnumerator<KeyValuePair<string, TowerData>> GetEnumerator() => m_ConfigurationDictionary.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<TowerData> IEnumerable<TowerData>.GetEnumerator() => configurations.GetEnumerator();

        public int IndexOf(TowerData item) => configurations.IndexOf(item);

        public void Insert(int index, TowerData item) => configurations.Insert(index, item);

        public void RemoveAt(int index) => configurations.RemoveAt(index);

        public void Add(TowerData item) => configurations.Add(item);

        public void Clear() => configurations.Clear();

        public bool Contains(TowerData item) => configurations.Contains(item);

        public void CopyTo(TowerData[] array, int arrayIndex) => configurations.CopyTo(array, arrayIndex);

        public bool Remove(TowerData item) => configurations.Remove(item);
    }
}
