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
    public partial class AuditPage : BasePage, INavigatedPage {

        private AuditPageViewModel _viewModel;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public AuditPage() {
            InitializeComponent();

            BindingContext = _viewModel = new AuditPageViewModel();
        }

        /// <summary>
        /// INavigatedView implementation
        /// </summary>
        public void ApplyVisualChangesWhileNavigating() {
            _viewModel.HideMenuCommand.Execute(null);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnAppearing() {
            base.OnAppearing();

            ((IListenerViewModel)_viewModel).Subscribe();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDisappearing() {
            base.OnDisappearing();

            ((IListenerViewModel)_viewModel).UnSubscribe();
        }

        /// <summary>
        /// Occurs only for Android (not for iOS).
        /// False navigate out from page, true - keep staing in this page.
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed() {
            if (_viewModel.Menu.IsMenuVisible) {
                _viewModel.Menu.HideMenu();
            }
            else {
                //_viewModel.NavigateOneStepBack();
                _viewModel.AuditOneStepBackViewNavigation();
            }

            return true;
        }
    }
}