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
    public partial class WebViewPage : BasePage, INavigatedPage, IWebViewPage {

        private WebViewPageViewModel _viewModel;

        private string _srcPath;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public WebViewPage() {
            InitializeComponent();

            BindingContext = _viewModel = new WebViewPageViewModel();
        }

        /// <summary>
        /// Source path
        /// </summary>
        public string SrcPath {
            get => _srcPath;
            set {
                _srcPath = value;
                _webFrame_WebView.Source = value;
            }
        }

        /// <summary>
        /// INavigatedPage implementation
        /// </summary>
        public void ApplyVisualChangesWhileNavigating() { }

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
            _viewModel.OneStepBackViewNavigation();
            ExtendedWebViewImpact(true);

            return true;
        }

        /// <summary>
        /// IWebViewPage implementation
        /// Allow to fix web view bug with audio playback.
        /// True - webView is hide, false - visible
        /// <param name="isVisible"></param>
        public void ExtendedWebViewImpact(bool isVisible) {
            _webFrame_WebView.IsNotVisible = isVisible;
        }
    }
}