using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tpm.Helpers;
using tpm.NavigationFramework;
using Xamarin.Forms;

namespace tpm.ViewModels {
    public sealed class PdfWebViewViewerPageViewModel :BasePageViewModel, IViewModel {

        private bool _isOneStepBackAvailable = false;
        private string _actionBarHeader;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public PdfWebViewViewerPageViewModel() {
            //ClosePageCommand = new Command(() => OneStepBackViewNavigation());
            StepBackCommand = new Command(() => OneStepBackViewNavigation());

            IsOneStepBackAvailable = true;
            ActionBarHeader = _actionBarHeader = "Pdf viewing";
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand ClosePageCommand {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand StepBackCommand {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public override string ActionBarHeader {
            get => _actionBarHeader;
            protected set => SetProperty<string>(ref _actionBarHeader, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsOneStepBackAvailable {
            get => _isOneStepBackAvailable;
            private set => SetProperty<bool>(ref _isOneStepBackAvailable, value);
        }

        /// <summary>
        /// Provide back step navigation for WebViewPage
        /// </summary>
        public void OneStepBackViewNavigation() {
            BaseSingleton<PageSwitchingLogic>.Instance.NavigateOneStepBack();
        }
    }
}