using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tpm.DependencyServices;
using tpm.Helpers;
using tpm.Models.DataContainers.DataItems;
using tpm.NavigationFramework;
using Xamarin.Forms;

namespace tpm.ViewModels {
    public sealed class TrainingToolsMaterialsBlockViewViewModel : ViewModelBase, IViewModel {

        private TrainingToolsItem _trainingsToolItem;
        private bool _canExecuteGoThroughLinkCommand = true;
        private bool _webViewIsNotVisible = false;

        /// <summary>
        /// Public ctor.
        /// </summary>
        /// <param name="trainingsToolItem"></param>
        public TrainingToolsMaterialsBlockViewViewModel(TrainingToolsItem trainingsToolItem) {
            TrainingsToolItem = trainingsToolItem;

            GoThroughLinkCommand = new Command(async () => {
                string fullPath =
                    System.IO.Path.Combine(DependencyService.Get<IFileHelper>().TpmDictionaryPath, DependencyService.Get<IFileHelper>().DownloadedPdfFileName);

                if (DependencyService.Get<IFileHelper>().IsFileExists(fullPath)) {
                    BaseSingleton<PageSwitchingLogic>.Instance
                        .DisplayWebViewPage(fullPath, PageTypes.PdfWebViewViewerPage);
                } else {
                    ToggleGoThroughLinkCommandExecution(false);

                    if (await DependencyService.Get<IFileHelper>().DownloadSourceAsync(TrainingsToolItem.Src)) {
                        if (await DisplayAlert("Complete", "Download complete. Open pdf?", "Ok", "Cancel")) {
                            BaseSingleton<PageSwitchingLogic>.Instance
                                .DisplayWebViewPage(fullPath, PageTypes.PdfWebViewViewerPage);
                        }
                    }
                    else {
                        await DisplayAlert("Faild", "Download faild");
                    }

                    ToggleGoThroughLinkCommandExecution(true);
            }
            }, () => _canExecuteGoThroughLinkCommand);
        }

        /// <summary>
        /// 
        /// </summary>
        public TrainingToolsItem TrainingsToolItem {
            get => _trainingsToolItem;
            private set => SetProperty<TrainingToolsItem>(ref _trainingsToolItem, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand GoThroughLinkCommand {
            get;
            private set;
        }

        /// <summary>
        /// Allow to fix web view bug with audio playback.
        /// True - webView is hide, false - visible
        public bool WebViewIsNotVisible {
            get => _webViewIsNotVisible;
            set => SetProperty<bool>(ref _webViewIsNotVisible, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ToggleGoThroughLinkCommandExecution(bool canExecute) {
            _canExecuteGoThroughLinkCommand = canExecute;
            ((Command)GoThroughLinkCommand).ChangeCanExecute();
        }
    }
}