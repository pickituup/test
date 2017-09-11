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
using Android.Graphics;
using Android.Provider;
using System.IO;
using Android.Database;
using Android.Net;

namespace tpm.Droid.Helpers {
    public sealed class PictureUtils {

        public static readonly int IMAGE_WIDTH_RESTRICTION = 360;
        public static readonly int IMAGE_HEIGHT_RESTRICTION = 640;

        /// <summary>
        ///  A Bitmap is a simple object that stores literal pixel data.That means that even if the original 
        ///  file was compressed, there is no compression in the Bitmap itself.
        ///  Need to scale the bitmap down by hand.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="destWidth"></param>
        /// <param name="destHeight"></param>
        /// <returns></returns>
        public static Bitmap GetScaledBitmap(Android.Net.Uri uri, int destWidth, int destHeight, Context context) {
            string fullPath = GetFileFullPathAlternative(uri, context);

            // Read in the dimensions of the image on disk
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InJustDecodeBounds = true;
            BitmapFactory.DecodeFile(fullPath, options);

            float srcWidth = options.OutWidth;
            float srcHeight = options.OutHeight;

            // Figure out how much to scale down by
            int inSampleSize = 1;

            if (srcHeight >= destHeight || srcWidth >= destWidth) {
                if (srcHeight >= destHeight) {
                    inSampleSize = (int)Math.Round(srcHeight / destHeight);
                }
                else {
                    inSampleSize = (int)Math.Round(srcWidth / destWidth);
                }
            }

            options = new BitmapFactory.Options();
            options.InSampleSize = inSampleSize;

            // Read in and create final bitmap
            return BitmapFactory.DecodeFile(fullPath, options);
        }

        /// <summary>
        /// This approach doesnt work on some android devices
        /// </summary>
        /// <returns></returns>
        //private static string GetFileFullPath(Android.Net.Uri uri, Context context) {
        //    string[] proj = { MediaStore.Images.Media.InterfaceConsts.Data };

        //    ICursor cursor = context.ContentResolver.Query(uri, proj, null, null, null);
        //    int column_index = cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.Data);
        //    cursor.MoveToFirst();
        //    string path = cursor.GetString(column_index);
            
        //    return path;
        //}

        /// <summary>
        /// Allow to get full path to the data on all android devices
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private static string GetFileFullPathAlternative(Android.Net.Uri uri, Context context) {
            string doc_id = "";

            using (var c1 = context.ContentResolver.Query(uri, null, null, null, null)) {
                c1.MoveToFirst();
                String document_id = c1.GetString(0);
                doc_id = document_id.Substring(document_id.LastIndexOf(":") + 1);
            }

            string path = "";

            string selection = Android.Provider.MediaStore.Images.Media.InterfaceConsts.Id + " =? ";
            using (ICursor cursor = context.ContentResolver.Query(Android.Provider.MediaStore.Images.Media.ExternalContentUri, null, selection, new string[] { doc_id }, null)) {
                if (cursor == null) return path;
                var columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
                cursor.MoveToFirst();
                path = cursor.GetString(columnIndex);
            }

            return path;
        }
    }
}