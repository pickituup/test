using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.Views.Pages {

    /// <summary>
    /// Represents pages with web view inside
    /// </summary>
    public interface IWebViewPage {

        /// <summary>
        /// Allow to fix web view bug with audio playback.
        /// True - webView is hide, false - visible</summary>
        /// <param name="isVisible"></param>
        void ExtendedWebViewImpact(bool isVisible);
    }
}
