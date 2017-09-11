using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.DependencyServices;
using tpm.Helpers;
using tpm.NavigationFramework;
using Xamarin.Forms;

namespace tpm.ViewModels {
    public sealed class WebViewPageViewModel : ViewModelBase, IViewModel {

        /// <summary>
        /// Public ctor.
        /// </summary>
        public WebViewPageViewModel() { }

        /// <summary>
        /// Provide back step navigation for WebViewPage
        /// </summary>
        public void OneStepBackViewNavigation() {
            BaseSingleton<PageSwitchingLogic>.Instance.NavigateOneStepBack();
            DependencyService.Get<IScreenService>().DefaultOrientation();
        }
    }
}
