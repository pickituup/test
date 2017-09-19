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
    public partial class PdfWebViewViewerPage : BasePage, INavigatedPage, IWebViewPage {

        private PdfWebViewViewerPageViewModel _viewModel;
        private string _srcPath;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public PdfWebViewViewerPage() {
            InitializeComponent();

            BindingContext = _viewModel = new PdfWebViewViewerPageViewModel();
        }

        /// <summary>
        /// Source path
        /// </summary>
        public string SrcPath {
            get => _srcPath;
            set {
                _srcPath = value;
                _pdfViewer_PdfWebViewViewer.Uri = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ApplyVisualChangesWhileNavigating() { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed() {
            _viewModel.OneStepBackViewNavigation();

            return true;
        }
    }
}
