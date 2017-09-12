using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.Helpers;
using tpm.Models.DataContainers;
using tpm.Models.DataContainers.DataItems;
using tpm.NavigationFramework;

namespace tpm.ViewModels {
    public sealed class WebinarsPageViewModel : BasePageViewModel, IViewModel {

        private WebinarsContainer _webinarsContainer;
        private string _actionBarHeader;
        private List<WebinarBlockViewViewModel> _webinars = new List<WebinarBlockViewViewModel>();

        /// <summary>
        /// Public ctor.
        /// </summary>
        public WebinarsPageViewModel() {
            _webinarsContainer = new WebinarsContainer();

            _webinars.AddRange(_webinarsContainer.BuildWebinarItems()
                .Select<WebinarItem, WebinarBlockViewViewModel>(w => new WebinarBlockViewViewModel(w)));

            MenuItems.First(i => i.PageType == PageTypes.WebinarsPage).IsSelected = true;
            ActionBarHeader = _actionBarHeader = "Webinars";
        }

        /// <summary>
        /// 
        /// </summary>
        public List<WebinarBlockViewViewModel> Webinars {
            get => _webinars;
            private set => SetProperty<List<WebinarBlockViewViewModel>>(ref _webinars, value);
        }

        /// <summary>
        /// BasePageViewModel implementation.
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