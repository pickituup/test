using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Helpers.Builders.ViewBuilders;

namespace tpm.NavigationFramework {
    public interface INavigatedPage : IView {

        /// <summary>
        /// Can apply some visual changes of current page through navigating process
        /// </summary>
        void ApplyVisualChangesWhileNavigating();
    }
}