using MessageUI;
using System;
using System.Collections.Generic;
using System.Text;
using tpm.DependencyServices;

[assembly: Xamarin.Forms.Dependency(typeof(tpm.iOS.Services.UseExternalComponentService))]
namespace tpm.iOS.Services {
    public sealed class UseExternalComponentService : IUseExternalComponentService {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void IntentToDisplayRelativeFile(string fileName) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        public void IntentToOpenWebSource(string src) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        public void IntentToSentMailWithPDF(string email) {
            string docsPath = FileHelperService.GetInternalTpmDirPath();
            string fullFilePath = System.IO.Path.Combine(docsPath, FileHelperService.GENERATED_PDF_FILE_NAME);

            if (MFMailComposeViewController.CanSendMail) {
                MFMailComposeViewController controller = new MFMailComposeViewController();
                controller.SetToRecipients(new string[] { email });
                controller.AddAttachmentData(Foundation.NSData.FromFile(fullFilePath), "@application/pdf,", FileHelperService.GENERATED_PDF_FILE_NAME);

                controller.Finished += (object sender, MFComposeResultEventArgs e) => {
                    e.Controller.DismissViewController(true, null);
                };
            }
        }
    }
}
