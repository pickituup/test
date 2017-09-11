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
    public partial class AboutPage : BasePage, INavigatedPage {

        private AboutPageViewModel _viewModel;

        /// <summary>
        /// 
        /// </summary>
        public AboutPage() {
            InitializeComponent();

            BindingContext = _viewModel = new AboutPageViewModel();
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
        /// <returns></returns>
        protected override bool OnBackButtonPressed() {
            if (_viewModel.Menu.IsMenuVisible) {
                _viewModel.Menu.HideMenu();
            }
            else {
                //_viewModel.NavigateOneStepBack();
                _viewModel.OneStepBackViewNavigation();
            }

            return true;
        }
    }
}