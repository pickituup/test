using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Helpers;
using tpm.NavigationFramework;

namespace tpm.ViewModels {
    public class AboutPageViewModel : BasePageViewModel, IViewModel {

        private string _actionBarHeader;

        public AboutPageViewModel() {
            MenuItems.First(i => i.PageType == PageTypes.AboutPage).IsSelected = true;
            ActionBarHeader = _actionBarHeader = "About";
        }

        /// <summary>
        /// BasePageViewModel implementation
        /// </summary>
        public override string ActionBarHeader {
            get => _actionBarHeader;
            protected set => SetProperty<string>(ref _actionBarHeader, value);
        }

        /// <summary>
        /// Provide back step navigation for WebViewPage
        /// </summary>
        public void OneStepBackViewNavigation() {
            BaseSingleton<PageSwitchingLogic>.Instance.NavigateOneStepBack();
        }
    }
}
