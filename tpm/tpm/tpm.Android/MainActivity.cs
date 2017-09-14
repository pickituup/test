using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Provider;
using Android.Support.V4.Content;
using Android.Util;
using Java.Lang;
using System.IO;
using System.Threading.Tasks;
using tpm.Droid.Helpers;

namespace tpm.Droid {
    [Activity(Icon = "@drawable/icon",
        Label = "@string/app_name",
        MainLauncher = false, Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation),]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {

        private static readonly int _CATCH_IMAGE_REQUEST_CODE = 1;
        private string _capturedPictureFilePath;
        
        /// <summary>
        /// TODO: try to define better solution
        /// </summary>
        public static TaskCompletionSource<string> CatchImageTaskCompletion { get; set; }

        /// <summary>
        /// TODO: refactor
        /// </summary>
        public void CatchPhoto() {
            CatchImageTaskCompletion = new TaskCompletionSource<string>();

            Intent intentPickImage = new Intent();
            intentPickImage.SetType("image/*");
            intentPickImage.SetAction(Intent.ActionGetContent);

            Intent takePictureIntent = new Intent();
            takePictureIntent.SetAction(MediaStore.ActionImageCapture);
            Java.IO.File imageOutput =
                Java.IO.File.CreateTempFile(
                    string.Format("tpm_pic_{0:dd MMM H:mm}", System.DateTime.Now),
                    ".png",
                    new Java.IO.File(DependencyServices.FileHelper.GetExternalTpmDirPath()));
            _capturedPictureFilePath = imageOutput.AbsolutePath;
            takePictureIntent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(imageOutput));

            Intent chooserIntent = Intent.CreateChooser(intentPickImage, "Select with");
            chooserIntent.PutExtra(Intent.ExtraInitialIntents, new Intent[] { takePictureIntent });

            StartActivityForResult(chooserIntent, _CATCH_IMAGE_REQUEST_CODE);
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
                if (requestCode == _CATCH_IMAGE_REQUEST_CODE) {
                    try {
                        if (data == null) {
                            ExtractImagePickedFromCamera(data);
                        }
                        else {
                            ExtractImagePickedFromGalery(data);
                        }
                    }
                    catch (System.Exception) {
                        CatchImageTaskCompletion.SetResult(null);
                    }
                }
            }
            else if (resultCode == Result.Canceled) {
                if (requestCode == _CATCH_IMAGE_REQUEST_CODE) {
                    CatchImageTaskCompletion.SetResult(null);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        private void ExtractImagePickedFromGalery(Intent data) {
            Bitmap bitmap = PictureUtils.GetScaledBitmap(
                    data.Data,
                    PictureUtils.IMAGE_WIDTH_RESTRICTION,
                    PictureUtils.IMAGE_HEIGHT_RESTRICTION,
                    this);

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

        /// <summary>
        /// TODO: refactor.
        /// </summary>
        /// <param name="data"></param>
        private void ExtractImagePickedFromCamera(Intent data) {
            BitmapFactory.Options bmOptions = new BitmapFactory.Options();
            bmOptions.InJustDecodeBounds = true;
            BitmapFactory.DecodeFile(_capturedPictureFilePath, bmOptions);
            int photoW = bmOptions.OutWidth;
            int photoH = bmOptions.OutHeight;

            // Determine how much to scale down the image
            int scaleFactor = Math.Min(photoW / PictureUtils.IMAGE_WIDTH_RESTRICTION, photoH / PictureUtils.IMAGE_HEIGHT_RESTRICTION);

            // Decode the image file into a Bitmap sized to fill the View
            bmOptions.InJustDecodeBounds = false;
            bmOptions.InSampleSize = scaleFactor;
            bmOptions.InPurgeable = true;

            Bitmap bitmap = BitmapFactory.DecodeFile(_capturedPictureFilePath, bmOptions);
            _capturedPictureFilePath = null;

            byte[] bytes;

            using (MemoryStream memoryStream = new MemoryStream()) {
                bitmap.Compress(Bitmap.CompressFormat.Png, 10, memoryStream);
                bytes = memoryStream.GetBuffer();
            }

            string base64String = Base64.EncodeToString(bytes, Base64Flags.Default);

            CatchImageTaskCompletion.SetResult(base64String);
        }
    }
}