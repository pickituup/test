using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using tpm.Helpers;
using tpm.Helpers.Observers.Audits;
using tpm.Validtion;

namespace tpm.ViewModels {
    public sealed class PublishViewViewModel : ViewModelBase, IViewModel {

        private PublishValidation _publishValidation = new PublishValidation();
        private string _emailAddress;

        /// <summary>
        /// Public ctor.
        /// </summary>
        public PublishViewViewModel() {
            //
            // TODO: remove after testiong
            //
            //EmailAddress = "emanueltestemail@gmail.com";

            HomeCommand = new Command(() => {
                BaseSingleton<AuditScopeObserver>.Instance.OnGoHome();
            });

            PublishCommand = new Command(async () => {
                if (EmailAddress != null) {
                    string validationOutput;

                    if (_publishValidation.IsValidEmail(EmailAddress, out validationOutput)) {
                        BaseSingleton<AuditScopeObserver>.Instance.OnPublish(EmailAddress);

                        EmailAddress = string.Empty;
                    }
                    else {
                        await DisplayAlert("Publish", validationOutput);
                    }
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand HomeCommand {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand PublishCommand {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string EmailAddress {
            get => _emailAddress;
            set => SetProperty<string>(ref _emailAddress, value);
        }
    }
}