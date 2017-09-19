using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tpm.DependencyServices;
using tpm.Helpers;
using tpm.NavigationFramework;
using Xamarin.Forms;

namespace tpm.ViewModels {
    public sealed class WebViewPageViewModel : ViewModelBase, IViewModel {

        /// <summary>
        /// Public ctor.
        /// </summary>
        public WebViewPageViewModel() {
            ClosePageCommand = new Command(() => OneStepBackViewNavigation());
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand ClosePageCommand {
            get;
            private set;
        }

        /// <summary>
        /// Provide back step navigation for WebViewPage
        /// </summary>
        public void OneStepBackViewNavigation() {
            DependencyService.Get<IScreenService>().DefaultOrientation();
            BaseSingleton<PageSwitchingLogic>.Instance.NavigateOneStepBack();
        }
    }
}