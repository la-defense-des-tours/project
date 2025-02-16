using Assets.Scripts.LaDefenseDesTours.Interfaces;
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
        /// <summary>
        /// The list of all the towers
        /// </summary>
        public List<TowerData> configurations;

        /// <summary>
        /// The internal reference to the dictionary made from the list of towers
        /// with the name of tower as the key
        /// </summary>
        Dictionary<string, TowerData> m_ConfigurationDictionary;


        /// <summary>
        /// The accessor to the towers by index
        /// </summary>
        /// <param name="index"></param>
        public TowerData this[int index]
        {
            get { return configurations[index]; }

        }

        public TowerData this[string key]
        {
            get { return m_ConfigurationDictionary[key]; }
            set { m_ConfigurationDictionary[key] = value; }
        }

        public void OnBeforeSerialize()
        {
        }

        /// <summary>
		/// Convert the list (m_Configurations) to a dictionary for access via name
		/// </summary>
		public void OnAfterDeserialize()
        {
            if (configurations == null)
            {
                return;
            }
            m_ConfigurationDictionary = configurations.ToDictionary(t => t.towerName);
        }

        public bool ContainsKey(string key)
        {
            return m_ConfigurationDictionary.ContainsKey(key);
        }

        public void Add(string key, TowerData value)
        {
            m_ConfigurationDictionary.Add(key, value);
        }

        public bool Remove(string key)
        {
            return m_ConfigurationDictionary.Remove(key);
        }

        public bool TryGetValue(string key, out TowerData value)
        {
            return m_ConfigurationDictionary.TryGetValue(key, out value);
        }

        TowerData IDictionary<string, TowerData>.this[string key]
        {
            get { return m_ConfigurationDictionary[key]; }
            set { m_ConfigurationDictionary[key] = value; }
        }

        public ICollection<string> Keys
        {
            get { return ((IDictionary<string, TowerData>)m_ConfigurationDictionary).Keys; }
        }

        ICollection<TowerData> IDictionary<string, TowerData>.Values
        {
            get { return m_ConfigurationDictionary.Values; }
        }
        IEnumerator<KeyValuePair<string, TowerData>> IEnumerable<KeyValuePair<string, TowerData>>.GetEnumerator()
        {
            return m_ConfigurationDictionary.GetEnumerator();
        }


        public void Add(KeyValuePair<string, TowerData> item)
        {
            m_ConfigurationDictionary.Add(item.Key, item.Value);
        }

        public bool Remove(KeyValuePair<string, TowerData> item)
        {
            return m_ConfigurationDictionary.Remove(item.Key);
        }

        public bool Contains(KeyValuePair<string, TowerData> item)
        {
            return m_ConfigurationDictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, TowerData>[] array, int arrayIndex)
        {
            int count = array.Length;
            for (int i = arrayIndex; i < count; i++)
            {
                TowerData config = configurations[i - arrayIndex];
                KeyValuePair<string, TowerData> current = new KeyValuePair<string, TowerData>(config.towerName, config);
                array[i] = current;
            }
        }

        public int IndexOf(TowerData item)
        {
            return configurations.IndexOf(item);
        }

        public void Insert(int index, TowerData item)
        {
            configurations.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            configurations.RemoveAt(index);
        }

        TowerData IList<TowerData>.this[int index]
        {
            get { return configurations[index]; }
            set { configurations[index] = value; }
        }

        public IEnumerator<TowerData> GetEnumerator()
        {
            return configurations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)configurations).GetEnumerator();
        }

        public void Add(TowerData item)
        {
            configurations.Add(item);
        }

        public void Clear()
        {
            configurations.Clear();
        }

        public bool Contains(TowerData item)
        {
            return configurations.Contains(item);
        }

        public void CopyTo(TowerData[] array, int arrayIndex)
        {
            configurations.CopyTo(array, arrayIndex);
        }

        public bool Remove(TowerData item)
        {
            return configurations.Remove(item);
        }

        public int Count
        {
            get { return configurations.Count; }
        }

        public bool IsReadOnly
        {
            get { return ((ICollection<TowerData>)configurations).IsReadOnly; }
        }
    }
}