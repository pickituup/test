using Android.App;
using Android.Content;
using Android.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using tpm.DependencyServices;
using tpm.Droid.Controlers.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(tpm.Droid.DependencyServices.FileHelper))]
namespace tpm.Droid.DependencyServices {
    public sealed class FileHelper : IFileHelper {

        private static readonly int _WORKER_SERVICE_NOTIFICATION_ID = 1;

        public static readonly string GENERATED_PDF_FILE_NAME = "tpm_own_collected_data.pdf";
        public static readonly string DOWNLOADED_PDF_FILE_NAME = "WalkWork_Surfaces.pdf";
        public static readonly string TPM_EXTERNAL_DICTIONARY_NAME = "TPM";

        private Notification.Builder _mainNotificationBuilder;
        private NotificationManager _notificationManager;

        /// <summary>
        /// 
        /// </summary>
        public FileHelper() {
            _mainNotificationBuilder = new Notification.Builder(Xamarin.Forms.Forms.Context)
                .SetSmallIcon(Resource.Drawable.ic_tpm_notification)
                .SetLargeIcon(BitmapFactory.DecodeResource(Xamarin.Forms.Forms.Context.Resources, Resource.Drawable.ic_tpm_notification))
                .SetTicker("Ticker text")
                .SetContentTitle("Content title")
                .SetContentText("Content text");

            _notificationManager = (NotificationManager)Xamarin.Forms.Forms.Context.GetSystemService(Context.NotificationService);
        }

        /// <summary>
        /// 
        /// </summary>
        public string GeneratedPdfFileName {
            get => GENERATED_PDF_FILE_NAME;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DownloadedPdfFileName {
            get => DOWNLOADED_PDF_FILE_NAME;
        }

        /// <summary>
        /// 
        /// </summary>
        public string TpmDictionaryPath {
            get {
                return GetExternalTpmDirPath();
            }
        }

        /// <summary>
        /// Get path to the TMP storaged Documents directory.
        /// </summary>
        /// <returns></returns>
        public static string GetExternalTpmDirPath() {
            string dirPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            string targetDir = System.IO.Path.Combine(dirPath, TPM_EXTERNAL_DICTIONARY_NAME);

            bool isDirExist = Directory.Exists(targetDir);

            if (isDirExist) {
                return targetDir;
            }
            else {
                Directory.CreateDirectory(targetDir);
            }

            return targetDir;
        }

        /// <summary>
        /// Returns FileStream for PDF
        /// </summary>
        /// <param name="fileName"></param>
        public Stream GetFileStreamForPDF() {
            string docsPath = GetExternalTpmDirPath();
            string fullFilePath = System.IO.Path.Combine(docsPath, GENERATED_PDF_FILE_NAME);

            FileStream fileStream = File.Open(fullFilePath, FileMode.Create);
            //
            // Clear all data from that file.
            //
            fileStream.SetLength(0);

            return fileStream;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        public Task<bool> DownloadSourceAsync(string src) {
            Notify("Downloading the source", "TPM", "Downloading", null);

            return Task<bool>.Run(() => {
                try {
                    using (HttpClient client = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler()))
                    using (Stream responseStream = client.GetStreamAsync(src).Result)
                    using (FileStream fileStream = System.IO.File.Open(System.IO.Path.Combine(GetExternalTpmDirPath(), DOWNLOADED_PDF_FILE_NAME), FileMode.Create)) {
                        //
                        // Clear all data from that file.
                        //
                        fileStream.SetLength(0);
                        responseStream.CopyTo(fileStream);
                    }

                    Notify("Downloading the source", "TPM", "Download complete", InitPendingIntent());

                    return true;
                }
                catch (Exception e) {
                    Notify("Downloading the source", "TPM", "Download faild", null);

                    return false;
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool IsFileExists(string fullFilePath) {
            return File.Exists(fullFilePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="contentTitle"></param>
        /// <param name="contentText"></param>
        private void Notify(string ticker, string contentTitle, string contentText, Intent intent) {
            _mainNotificationBuilder
                .SetTicker(ticker)
                .SetContentTitle(contentTitle)
                .SetContentText(contentText)
                .SetAutoCancel(true);

            if (intent != null) {
                PendingIntent pendingIntent = PendingIntent.GetActivity(Xamarin.Forms.Forms.Context, 0, intent, 0);
                _mainNotificationBuilder.SetContentIntent(pendingIntent);
            }

            _notificationManager.Notify(_WORKER_SERVICE_NOTIFICATION_ID, _mainNotificationBuilder.Build());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Intent InitPendingIntent() {
            string docsPath = GetExternalTpmDirPath();
            string fullFilePath = System.IO.Path.Combine(docsPath, DOWNLOADED_PDF_FILE_NAME);

            Java.IO.File file = new Java.IO.File(fullFilePath);
            file.SetReadable(true, false);

            Android.Net.Uri pdfUrl = Android.Net.Uri.FromFile(file);

            Intent intent = new Intent();
            intent.SetDataAndType(pdfUrl, "application/pdf");
            intent.SetFlags(ActivityFlags.ClearTop);

            return Intent.CreateChooser(intent, "Open with");
        }

        /// <summary>
        /// Delete this method if not use.
        /// It should working when your HttpClient implementation - AndroidClientHandler and
        /// SSL/TLS implementation - Native TLS 1.2+. Look at Android advanced options.
        /// https://forums.xamarin.com/discussion/10405/the-authentication-or-decryption-has-failed-in-the-web-request/p2
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private void DownloadPdfWithHttpRequestOldWorkingVersion(string src) {
            HttpWebRequest request = new HttpWebRequest(new Uri(src));
            request.ContentType = "application/pdf";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (FileStream fileStream = System.IO.File.Open(System.IO.Path.Combine(GetExternalTpmDirPath(), DOWNLOADED_PDF_FILE_NAME), FileMode.Create)) {
                //
                // Clear all data from that file.
                //
                fileStream.SetLength(0);
                stream.CopyTo(fileStream);
            }
        }
    }
}