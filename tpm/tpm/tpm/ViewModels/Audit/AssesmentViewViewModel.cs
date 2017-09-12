using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using tpm.Helpers;
using tpm.Helpers.Observers.Audits;
using tpm.NavigationFramework;

namespace tpm.ViewModels {
    public class AssesmentViewViewModel : ViewModelBase {

        /// <summary>
        /// Public ctor
        /// </summary>
        public AssesmentViewViewModel() {
            StartAssesmentCommand = new Command(() => {
                BaseSingleton<AuditScopeObserver>.Instance.OnStartAssesment();
            });

            MoreInfoCommand = new Command(() => {
                BaseSingleton<PageSwitchingLogic>.Instance.NavigateTo(PageTypes.AboutPage);
            });
        }

        /// <summary>
        /// Start assesment command
        /// </summary>
        public ICommand StartAssesmentCommand {
            get;
            private set;
        }

        /// <summary>
        /// Start assesment command
        /// </summary>
        public ICommand MoreInfoCommand {
            get;
            private set;
        }
    }
}
