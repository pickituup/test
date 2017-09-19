using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpm.Views.Pages {

    /// <summary>
    /// Allow to stop audio loading when parent page where 
    /// closed (android web view still runing audio when page with appropriate WebVeiw was closed or stoped).
    /// </summary>
    public interface IWebViewBugPage {

        /// <summary>
        /// Allow to fix web view bug with audio playback.
        /// True - webView is hide, false - visible</summary>
        /// <param name="isVisible"></param>
        void ExtendedWebViewImpact(bool isVisible);
    }
}
