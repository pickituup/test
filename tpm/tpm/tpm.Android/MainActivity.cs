using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Provider;
using Android.Util;
using System.IO;
using System.Threading.Tasks;
using tpm.Droid.Helpers;

namespace tpm.Droid {
    [Activity(Icon = "@drawable/icon",
        Label = "@string/app_name",
        MainLauncher = false, Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation),]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {

        private static readonly int _CATCH_IMAGE__REQUEST_CODE = 1;

        /// <summary>
        /// TODO: try to define better solution
        /// </summary>
        public static TaskCompletionSource<string> CatchImageTaskCompletion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void CatchPhoto() {
            CatchImageTaskCompletion = new TaskCompletionSource<string>();

            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            
            StartActivityForResult(Intent.CreateChooser(intent, "Select with"), _CATCH_IMAGE__REQUEST_CODE);
        }

        protected override void OnPause() {
            base.OnPause();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bundle"></param>
        protected override void OnCreate(Bundle bundle) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="resultCode"></param>
        /// <param name="data"></param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data) {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok) {
                if (requestCode == _CATCH_IMAGE__REQUEST_CODE) {
                    try {
                        //
                        // TODO: defend by try/catch in fault return null - which also must be handled by the receiver!!!
                        //
                        Bitmap bitmap = PictureUtils.GetScaledBitmap(
                                data.Data,
                                PictureUtils.IMAGE_WIDTH_RESTRICTION,
                                PictureUtils.IMAGE_HEIGHT_RESTRICTION,
                                this); ;

                        if (bitmap == null) {
                            bitmap = MediaStore.Images.Media.GetBitmap(ContentResolver, data.Data);
                        }

                        byte[] bytes;

                        using (MemoryStream memoryStream = new MemoryStream()) {
                            bitmap.Compress(Bitmap.CompressFormat.Png, 10, memoryStream);
                            bytes = memoryStream.GetBuffer();
                        }

                        string base64String = Base64.EncodeToString(bytes, Base64Flags.Default);

                        CatchImageTaskCompletion.SetResult(base64String);
                    }
                    catch (System.Exception) {
                        CatchImageTaskCompletion.SetResult(null);
                    }
                }
            }
            else if (resultCode == Result.Canceled) {
                if (requestCode == _CATCH_IMAGE__REQUEST_CODE) {
                    CatchImageTaskCompletion.SetResult(null);
                }
            }
        }
    }
}