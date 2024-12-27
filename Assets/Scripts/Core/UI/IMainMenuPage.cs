using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.UI
{
    /// <summary>
    /// Base interface for menu pages
    /// </summary>
    public interface IMainMenuPage
    {
        /// <summary>
        /// Deactivates this page
        /// </summary>
        void Hide();

        /// <summary>
        /// Activates this page
        /// </summary>
        void Show();
    }
}
