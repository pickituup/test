using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.ViewModels;
using tpm.NavigationFramework;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tpm.Views.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebinarsPage : BasePage, INavigatedPage {

        private WebinarsPageViewModel _viewModel;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public WebinarsPage() {
            InitializeComponent();

            BindingContext = _viewModel = new WebinarsPageViewModel();
        }

        /// <summary>
        /// 
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
                _viewModel.OneStepBackViewNavigation();
            }

            return true;
        }
    }
}