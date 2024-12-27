using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Data
{
    /// <summary>
    /// Json implementation of file saver
    /// </summary>
    public class JsonSaver<T> : FileSaver<T> where T : IDataStore
    {
        public JsonSaver(string filename)
            : base(filename)
        {
        }

        /// <summary>
        /// Save the specified data store
        /// </summary>
        public override void Save(T data)
        {
            string json = JsonUtility.ToJson(data);

            using (StreamWriter writer = GetWriteStream())
            {
                writer.Write(json);
            }
        }

        /// <summary>
        /// Load the specified data store
        /// </summary>
        public override bool Load(out T data)
        {
            if (!File.Exists(m_Filename))
            {
                data = default(T);
                return false;
            }

            using (StreamReader reader = GetReadStream())
            {
                data = JsonUtility.FromJson<T>(reader.ReadToEnd());
            }

            return true;
        }
    }
}
