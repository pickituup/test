using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tpm.DependencyServices;
using tpm.Models.DataContainers.DataItems;
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
                if (trainingsToolItem.DownloadSrc) {
                    _canExecuteGoThroughLinkCommand = false;
                    ((Command)GoThroughLinkCommand).ChangeCanExecute();

                    bool result = await DependencyService.Get<IFileHelper>().DownloadSourceAsync(TrainingsToolItem.Src);

                    if (result) {
                        bool openFile = await DisplayAlert("Download", "Source is downloaded. Open src?", "Ok", "Cancel");
                        if (openFile) {
                            DependencyService.Get<IUseExternalComponentService>().IntentToDisplayRelativeFile(DependencyService.Get<IFileHelper>().DownloadedPdfFileName);
                        }
                    }
                    else {
                        await DisplayAlert("Download", "Download faild");
                    }

                    _canExecuteGoThroughLinkCommand = true;
                    ((Command)GoThroughLinkCommand).ChangeCanExecute();
                }
                else {
                    DependencyService.Get<IUseExternalComponentService>().IntentToOpenWebSource(TrainingsToolItem.Src);
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
    }
}