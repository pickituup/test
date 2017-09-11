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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using tpm.Droid.Effects;

[assembly: ResolutionGroupName("tpm.ToolKit.Effects")]
[assembly: ExportEffect(typeof(UnderlineEffect), nameof(UnderlineEffect))]
namespace tpm.Droid.Effects {
    public class UnderlineEffect : PlatformEffect {

        /// <summary>
        /// 
        /// </summary>
        protected override void OnAttached() {
            SetUnderline(true);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnDetached() {
            SetUnderline(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args) {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == Label.TextProperty.PropertyName || 
                args.PropertyName == Label.FormattedTextProperty.PropertyName) {
                SetUnderline(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="underlined"></param>
        private void SetUnderline(bool underlined) {
            try {
                var textView = (TextView)Control;
                if (underlined) {
                    textView.PaintFlags |= PaintFlags.UnderlineText;
                }
                else {
                    textView.PaintFlags &= ~PaintFlags.UnderlineText;
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Cannot underline Label. Error: ", ex.Message);
            }
        }
    }
}