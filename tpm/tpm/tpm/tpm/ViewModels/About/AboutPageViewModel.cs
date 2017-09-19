using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Helpers;
using tpm.NavigationFramework;
using tpm.DependencyServices;
using Xamarin.Forms;

namespace tpm.ViewModels {
    public class AboutPageViewModel : BasePageViewModel, IViewModel {

        private string _actionBarHeader;
        private string _versionNumber;
        private string _versionName;

        /// <summary>
        /// 
        /// </summary>
        public AboutPageViewModel() {
            MenuItems.First(i => i.PageType == PageTypes.AboutPage).IsSelected = true;
            ActionBarHeader = _actionBarHeader = "About";

            VersionNumber = DependencyService.Get<IPackageInfo>().VersionNumber;
            VersionName = DependencyService.Get<IPackageInfo>().VersionName;
        }

        /// <summary>
        /// 
        /// </summary>
        public string VersionNumber {
            get => _versionNumber;
            private set => SetProperty<string>(ref _versionNumber, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public string VersionName {
            get => _versionName;
            private set => SetProperty<string>(ref _versionName, value);
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
