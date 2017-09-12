using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.NavigationFramework;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tpm.ViewModels;

namespace tpm.Views.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrainingPage : BasePage, INavigatedPage, IWebViewBugPage {

        private TrainingPageViewModel _viewModel;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public TrainingPage() {
            InitializeComponent();

            BindingContext = _viewModel = new TrainingPageViewModel();
        }

        /// <summary>
        /// INavigatedPage implementation
        /// </summary>
        public void ApplyVisualChangesWhileNavigating() {
            _viewModel.HideMenuCommand.Execute(null);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnAppearing() {
            base.OnAppearing();

            ExtendedWebViewImpact(false);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDisappearing() {
            base.OnDisappearing();
            
            ExtendedWebViewImpact(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed() {
            if (_viewModel.Menu.IsMenuVisible) {
                _viewModel.Menu.HideMenu();
            }
            else {
                _viewModel.OneStepBackViewNavigation();
                ExtendedWebViewImpact(true);
            }

            return true;
        }

        /// <summary>
        /// IWebViewPage implementation
        /// Allow to fix web view bug with audio playback.
        /// True - webView is hide, false - visible
        /// <param name="isVisible"></param>
        public void ExtendedWebViewImpact(bool isVisible) {
            _viewModel.WEB_VIEW_BUG(isVisible);
        }
    }
}
