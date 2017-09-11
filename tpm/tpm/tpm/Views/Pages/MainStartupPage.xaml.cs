using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpm.NavigationFramework;
using tpm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tpm.Views.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainStartupPage : ContentPage, INavigatedPage {

        private MainStartupPageViewModel _viewModel;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public MainStartupPage() {
            InitializeComponent();

            BindingContext = _viewModel = new MainStartupPageViewModel();
        }

        /// <summary>
        /// INavigatedPage implementation
        /// </summary>
        public void ApplyVisualChangesWhileNavigating() {
            
        }
    }
}
