using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using tpm.DependencyServices;
using tpm.iOS.Services;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(PickImageService))]
namespace tpm.iOS.Services {
    public sealed class PickImageService : IPickImageService {

        public static readonly int IMAGE_WIDTH_RESTRICTION = 360;
        public static readonly int IMAGE_HEIGHT_RESTRICTION = 640;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<string> CatchPhoto() {
            string imageBase64 = null;
            Task<string> task = new Task<string>(() => imageBase64);

            UIViewController topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (topController.PresentedViewController != null) {
                topController = topController.PresentedViewController;
            }

            UIImagePickerController imagePicker = new UIImagePickerController();
            imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

            imagePicker.FinishedPickingMedia += (sender, args) => {
                if (args?.OriginalImage != null) {
                    UIImage originalImage = args.OriginalImage.Scale(new CGSize(IMAGE_WIDTH_RESTRICTION, IMAGE_HEIGHT_RESTRICTION));
                    imageBase64 = originalImage.AsPNG().GetBase64EncodedString(NSDataBase64EncodingOptions.EndLineWithLineFeed);
                }
                topController.DismissModalViewController(true);
                task.Start();
            };

            imagePicker.Canceled += (sender, args) => {
                topController.DismissModalViewController(true);
                task.Start();
            };

            topController.PresentModalViewController(imagePicker, true);

            return task;
        }
    }
}
