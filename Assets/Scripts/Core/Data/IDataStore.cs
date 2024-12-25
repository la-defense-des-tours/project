using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.Data
{
    /// <summary>
    /// Interface for data store
    /// </summary>
    public interface IDataStore
    {
        void PreSave();

        void PostLoad();
    }
}
