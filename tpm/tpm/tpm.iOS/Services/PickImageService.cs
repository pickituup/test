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

            try {
                UIViewController topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
                while (topController.PresentedViewController != null) {
                    topController = topController.PresentedViewController;
                }

                //
                // Action sheet
                //
                UIAlertController actionSheet = 
                    UIAlertController.Create("Pick photo.", "Pick photo with:", UIAlertControllerStyle.ActionSheet);

                //
                // Photo galery action
                //
                actionSheet.AddAction(UIAlertAction.Create("Item One", UIAlertActionStyle.Default, (action) => {
                    UIImagePickerController imagePicker = new UIImagePickerController();
                    imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
                    imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

                    imagePicker.FinishedPickingMedia += (sender, args) => {
                        if (args?.OriginalImage != null) {
                            //
                            // Figure out how much to scale down by
                            //
                            int inSampleSize = GetInSampleSize(args.OriginalImage.Size.Width, args.OriginalImage.Size.Height);

                            UIImage originalImage = args.OriginalImage.Scale(new CGSize(args.OriginalImage.Size.Width / inSampleSize, args.OriginalImage.Size.Height / inSampleSize));
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
                }));

                //
                // Camera action
                //
                actionSheet.AddAction(UIAlertAction.Create("Item Two", UIAlertActionStyle.Default, (action) => {
                    if (UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera)) {
                        UIImagePickerController imagePicker = new UIImagePickerController();
                        imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
                        imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.Camera);

                        imagePicker.FinishedPickingMedia += (sender, args) => {
                            if (args?.OriginalImage != null) {
                                //
                                // Figure out how much to scale down by
                                //
                                int inSampleSize = GetInSampleSize(args.OriginalImage.Size.Width, args.OriginalImage.Size.Height);

                                UIImage originalImage = args.OriginalImage.Scale(new CGSize(args.OriginalImage.Size.Width / inSampleSize, args.OriginalImage.Size.Height / inSampleSize));
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
                    }
                    else {
                        UIAlertController alert = UIAlertController.Create("Warning", "Your device don't have camera", UIAlertControllerStyle.Alert);
                        alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (alertAction) => {
                            imageBase64 = null;
                            task.Start();
                        }));

                        topController.PresentViewController(alert, true, null);
                    }
                }));


                UIPopoverPresentationController presentationPopover = actionSheet.PopoverPresentationController;
                if (presentationPopover != null) {
                    presentationPopover.SourceView = topController.View;
                    presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
                }

                // Display the alert
                topController.PresentViewController(actionSheet, true, null);
            }
            catch (Exception) {
                imageBase64 = null;
                task.Start();
            }
            return task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcWidth"></param>
        /// <param name="srcHeight"></param>
        /// <returns></returns>
        private int GetInSampleSize(nfloat srcWidth, nfloat srcHeight) {
            int inSampleSize = 1;

            if (srcHeight >= IMAGE_HEIGHT_RESTRICTION || srcWidth >= IMAGE_WIDTH_RESTRICTION) {
                if (srcHeight >= IMAGE_HEIGHT_RESTRICTION) {
                    inSampleSize = (int)Math.Round(srcHeight / IMAGE_HEIGHT_RESTRICTION);
                }
                else {
                    inSampleSize = (int)Math.Round(srcWidth / IMAGE_WIDTH_RESTRICTION);
                }
            }

            return inSampleSize;
        }

        /// <summary>
        /// Old working version. Only pictures from photo galery can be selected.
        /// </summary>
        /// <returns></returns>
        //public Task<string> CatchPhoto() {
        //    string imageBase64 = null;
        //    Task<string> task = new Task<string>(() => imageBase64);

        //    UIViewController topController = UIApplication.SharedApplication.KeyWindow.RootViewController;
        //    while (topController.PresentedViewController != null) {
        //        topController = topController.PresentedViewController;
        //    }

        //    UIImagePickerController imagePicker = new UIImagePickerController();
        //    imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
        //    imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);

        //    imagePicker.FinishedPickingMedia += (sender, args) => {
        //        if (args?.OriginalImage != null) {
        //            UIImage originalImage = args.OriginalImage.Scale(new CGSize(IMAGE_WIDTH_RESTRICTION, IMAGE_HEIGHT_RESTRICTION));
        //            imageBase64 = originalImage.AsPNG().GetBase64EncodedString(NSDataBase64EncodingOptions.EndLineWithLineFeed);
        //        }
        //        topController.DismissModalViewController(true);
        //        task.Start();
        //    };

        //    imagePicker.Canceled += (sender, args) => {
        //        topController.DismissModalViewController(true);
        //        task.Start();
        //    };

        //    topController.PresentModalViewController(imagePicker, true);

        //    return task;
        //}
    }
}