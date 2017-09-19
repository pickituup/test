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
using System.Net;
using Java.Net;
using Java.IO;
using System.IO;

namespace tpm.Droid.Controlers.Services {

    [Service(Label = "Tpm download")]
    public sealed class DownloadService : IntentService {

        public static readonly int UPDATE_PROGRESS = 8344;
        private static readonly string INTENT_STRING_SRC_EXTRA = "downloadservice.src";
        private static readonly string INTENT_RESULTRECEIVER_EXTRA = "downloadservice.resultreceiver";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static Intent InitIntent(Context context, ResultReceiver resultReceiver, string src) {
            Intent intent = new Intent(context, typeof(DownloadService));
            intent.PutExtra(INTENT_STRING_SRC_EXTRA, src);
            intent.PutExtra(INTENT_RESULTRECEIVER_EXTRA, resultReceiver);

            return intent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intent"></param>
        protected override void OnHandleIntent(Intent intent) {
            HttpWebRequest r = new HttpWebRequest(new Uri(intent.Extras.GetString(INTENT_STRING_SRC_EXTRA)));

            using (HttpWebResponse resp = (HttpWebResponse)r.GetResponse())
            using (Stream stream = resp.GetResponseStream()) {
                FileStream fileStream = System.IO.File.Create(string.Format("{0}/TEST.pdf", Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath));

                stream.CopyTo(fileStream);
                stream.Dispose();
            }
        }

        //private void Test(Intent intent) {
        //    string urlToDownload = intent.GetStringExtra(INTENT_STRING_SRC_EXTRA);
        //    //ResultReceiver receiver = (ResultReceiver)intent.GetParcelableExtra(INTENT_RESULTRECEIVER_EXTRA);

        //    try {
        //        URL url = new URL(urlToDownload);
        //        URLConnection connection = url.OpenConnection();
        //        connection.Connect();
        //        // this will be useful so that you can show a typical 0-100% progress bar
        //        int fileLength = connection.ContentLength;

        //        //    // download the file
        //        Java.IO.InputStream input = new Java.IO.BufferedInputStream(connection.InputStream);
        //        Java.IO.OutputStream output = new Java.IO.FileOutputStream(string.Format("{0}/FILE.ppt", Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads)));

        //        byte[] data = new byte[1024];
        //        long total = 0;
        //        int count;
        //        while ((count = input.Read(data)) != -1) {
        //            total += count;
        //            // publishing the progress....
        //            //Bundle resultData = new Bundle();
        //            //resultData.PutInt("progress", (int)(total * 100 / fileLength));
        //            //receiver.Send(UPDATE_PROGRESS, resultData);
        //            output.Write(data, 0, count);
        //        }

        //        output.Flush();
        //        output.Close();
        //        input.Close();
        //    }
        //    catch (IOException e) {
        //        e.PrintStackTrace();
        //    }

        //    //Bundle resultData = new Bundle();
        //    //resultData.putInt("progress", 100);
        //    //receiver.send(UPDATE_PROGRESS, resultData);
        //}
    }
}