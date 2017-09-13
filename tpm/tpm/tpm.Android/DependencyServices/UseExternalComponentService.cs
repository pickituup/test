using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using tpm.DependencyServices;
using System.Runtime.CompilerServices;
using tpm.Droid.Controlers.Services;
using System.IO;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(tpm.Droid.DependencyServices.UseExternalComponentService))]
namespace tpm.Droid.DependencyServices {

    /// <summary>
    /// 
    /// </summary>
    public sealed class UseExternalComponentService : IUseExternalComponentService {

        /// <summary>
        /// 
        /// </summary>
        public void IntentToSentMailWithPDF(string email) {
            string docsPath = FileHelper.GetExternalTpmDirPath();
            string fullFilePath = Path.Combine(docsPath, FileHelper.GENERATED_PDF_FILE_NAME);

            Java.IO.File file = new Java.IO.File(fullFilePath);
            file.SetReadable(true, false);

            Android.Net.Uri pdfUrl = Android.Net.Uri.FromFile(file);

            Intent emailIntent = new Intent(Android.Content.Intent.ActionSend);
            emailIntent.SetType("application/pdf");
            emailIntent.PutExtra(Android.Content.Intent.ExtraEmail, new String[] { email });
            emailIntent.PutExtra(Android.Content.Intent.ExtraSubject, "TPM subject");
            emailIntent.PutExtra(Android.Content.Intent.ExtraText, "TPM application");
            emailIntent.PutExtra(Intent.ExtraStream, pdfUrl);

            Xamarin.Forms.Forms.Context.StartActivity(Intent.CreateChooser(emailIntent, "Send mail..."));
        }

        /// <summary>
        /// Not used. Remove.
        /// </summary>
        /// <param name="src"></param>
        private void IntentToOpenWebSource(string src) {
            Intent intent = new Intent(Intent.ActionView);
            intent.SetData(Android.Net.Uri.Parse(src));

            Xamarin.Forms.Forms.Context.StartActivity(Intent.CreateChooser(intent, "Open with"));
        }

        /// <summary>
        /// Not used. Remove.
        /// </summary>
        private void IntentToDisplayRelativeFile(string fileName) {
            string docsPath = FileHelper.GetExternalTpmDirPath();
            string fullFilePath = Path.Combine(docsPath, fileName);

            Java.IO.File file = new Java.IO.File(fullFilePath);
            file.SetReadable(true, false);

            Android.Net.Uri pdfUrl = Android.Net.Uri.FromFile(file);

            Intent intent = new Intent();
            intent.SetDataAndType(pdfUrl, "application/pdf");
            intent.SetFlags(ActivityFlags.ClearTop);

            Forms.Context.StartActivity(Intent.CreateChooser(intent, "Open with:"));
        }
    }
}