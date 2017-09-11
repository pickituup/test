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
    public class WebinarBlockViewViewModel : ViewModelBase, IViewModel {

        private WebinarItem _webinar;

        /// <summary>
        /// Public ctor.
        /// </summary>
        /// <param name="webinar"></param>
        public WebinarBlockViewViewModel(WebinarItem webinar) {
            Webinar = webinar;

            PlayVideoCommand = new Command(() => {
                BaseSingleton<PageSwitchingLogic>.Instance.DisplayWebViewPage(Webinar.SourcePath);
                DependencyService.Get<IScreenService>().LandscapeFullScreen();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand PlayVideoCommand {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public WebinarItem Webinar {
            get => _webinar;
            private set => SetProperty<WebinarItem>(ref _webinar, value);
        }
    }
}