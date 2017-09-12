using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Helpers;
using tpm.NavigationFramework;

namespace tpm.ViewModels {
    public sealed class PdfWebViewViewerPageViewModel: ViewModelBase, IViewModel {

        /// <summary>
        /// Public ctor.
        /// </summary>
        public PdfWebViewViewerPageViewModel() { }

        /// <summary>
        /// Provide back step navigation for WebViewPage
        /// </summary>
        public void OneStepBackViewNavigation() {
            BaseSingleton<PageSwitchingLogic>.Instance.NavigateOneStepBack();
        }
    }
}